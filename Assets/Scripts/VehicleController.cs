using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider frontRightCollider; 
    public WheelCollider frontLeftCollider;
    public WheelCollider backRightCollider;
    public WheelCollider backLeftCollider;

    [Header("Wheel Transforms")]
    public Transform frontRightTransform;
    public Transform frontLeftTransform;
    public Transform backRightTransform;
    public Transform backLeftTransform;
    public GameObject vehicleDoor;

    [Header("Vehicle Engine")]
    public float maxAcceleration = 100f;
    public float maxBrakingForce = 200f;
    private float currentAcceleration = 0f;
    private float currentBrakingForce = 0f;

    [Header("Vehicle Steering")]
    public float maxWheelTurnAngle = 20f;
    private float currentWheelTurnAngle = 0f;

    [Header("Vehicle Interactions")]
    private bool isInVehicle;
    private float interactionRadius = 5f;

    [Header("Player Components")]
    public GameObject ThirdPersonCam;
    public GameObject ThirdPersonCanvas;
    public GameObject AimCam;
    public GameObject AimCanvas;
    public GameObject Player;
    public GameObject objective4;

    [Header("Vehicle Hitting")]
    public float hitRange = 3f;
    private float hitDamage = 100f;
    public GameObject bloodEffect;
    public GameObject destroyEffect;
    public Camera cam;

    //Vector3 bound;

    private void Awake()
    {
        //bound = gameObject.transform.eulerAngles;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //bound = transform.eulerAngles;
        //if (gameObject.transform.eulerAngles.z < -35)
        //{
        //    bound.z = -35;
        //    gameObject.transform.eulerAngles = bound;
        //}
        
        //if (transform.eulerAngles.z > 35)
        //{
        //    bound.z = 35;
        //    gameObject.transform.eulerAngles = bound;
        //}


        if (isInVehicle)
        {
            Player.SetActive(false);
            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            AimCam.SetActive(false);
            AimCanvas.SetActive(false);
            objective4.SetActive(true);

            MoveVehicle();
            VehicleSteering();
            ApplyBrakes();
            HitZombies();
        }
        else
        {
            Player.SetActive(true);
            ThirdPersonCam.SetActive(true);
            ThirdPersonCanvas.SetActive(true);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);
            objective4.SetActive(false);
        }


        if(!isInVehicle && Vector3.Distance(transform.position, Player.transform.position) < interactionRadius && Input.GetKeyDown(KeyCode.F))
        {
              isInVehicle = true;
            ObjectiveComplete.instance.Objective3Done();
        }
        else if(isInVehicle && Input.GetKeyDown(KeyCode.F))
        {
            isInVehicle = false;
            Player.transform.position = vehicleDoor.transform.position;
        }

    }

    private void MoveVehicle()
    {
        frontRightCollider.motorTorque = currentAcceleration;
        frontLeftCollider.motorTorque = currentAcceleration;
        backRightCollider.motorTorque = currentAcceleration;
        backLeftCollider.motorTorque = currentAcceleration;

        currentAcceleration = maxAcceleration * -Input.GetAxis("Vertical");
    }

    private void VehicleSteering()
    {
        currentWheelTurnAngle = maxWheelTurnAngle * Input.GetAxis("Horizontal");
        frontRightCollider.steerAngle = currentWheelTurnAngle;
        frontLeftCollider.steerAngle = currentWheelTurnAngle;


        // wheel animation
        SteerWheel(frontRightCollider, frontRightTransform);
        SteerWheel(frontLeftCollider, frontLeftTransform);
        SteerWheel(backRightCollider, backRightTransform);
        SteerWheel(backLeftCollider, backLeftTransform);

    }

    private void SteerWheel(WheelCollider wC, Transform wT)
    {
        Vector3 position;
        Quaternion rotation;

        wC.GetWorldPose(out position, out rotation);

        wT.position = position;
        wT.rotation = rotation;

    }

    void ApplyBrakes()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            currentBrakingForce = maxBrakingForce;
        }
        else
        {
            currentBrakingForce = 0f;
        }
        frontRightCollider.brakeTorque = currentBrakingForce;
        frontLeftCollider.brakeTorque = currentBrakingForce;
        backRightCollider.brakeTorque = currentBrakingForce;
        backLeftCollider.brakeTorque = currentBrakingForce;
    }
    void HitZombies()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, hitRange))
        {

            Zombie1 zombie1 = hitInfo.transform.GetComponent<Zombie1>();

            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();

            DamagableObject damagableObject = hitInfo.transform.GetComponent<DamagableObject>();

            if (zombie1 != null)
            {
                zombie1.ZombieDamaged(hitDamage);
                GameObject bloodEffectClone = Instantiate(bloodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                zombie1.GetComponent<CapsuleCollider>().enabled = false;
                Destroy(bloodEffectClone, 1f);
            }
            else if (zombie2 != null)
            {
                zombie2.ZombieDamaged(hitDamage);
                GameObject bloodEffectClone = Instantiate(bloodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                zombie2.GetComponent<CapsuleCollider>().enabled = false;
                Destroy(bloodEffectClone, 1f);
            }
            else if (damagableObject != null)
            {
                GameObject destroyEffectClone = Instantiate(destroyEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                damagableObject.HitDamage(hitDamage);
                Destroy(destroyEffectClone, 1f);
            }

        }
    }
}
