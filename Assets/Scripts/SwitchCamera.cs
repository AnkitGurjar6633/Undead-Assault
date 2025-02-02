using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [Header("Cameras and Crosshairs")]
    public GameObject aimCam; 
    public GameObject aimCamCrosshair; 
    public GameObject thirdPersonCam; 
    public GameObject thirdPersonCamCrosshair;

    public Animator animator;

    public PlayerMovementScript player;


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            //animator.SetBool("Idle", false);
            //animator.SetBool("IdleAim", true);
            //animator.SetBool("Walk", true);
            //animator.SetBool("RifleWalk", true);

            player.isAiming = true;


            aimCam.SetActive(true);
            aimCamCrosshair.SetActive(true);
            thirdPersonCam.SetActive(false);
            thirdPersonCamCrosshair.SetActive(false);
        }
        else if (Input.GetButton("Fire2"))
        {
            //animator.SetBool("Idle", false);
            //animator.SetBool("IdleAim", true);
            //animator.SetBool("Walk", false);
            //animator.SetBool("RifleWalk", false);

            player.isAiming = true;


            aimCam.SetActive(true);
            aimCamCrosshair.SetActive(true);
            thirdPersonCam.SetActive(false);
            thirdPersonCamCrosshair.SetActive(false);
        }
        else
        {
            //animator.SetBool("Idle", true);
            //animator.SetBool("IdleAim", false); 
            //animator.SetBool("RifleWalk", false);

            player.isAiming = false;


            aimCam.SetActive(false);
            aimCamCrosshair.SetActive(false);
            thirdPersonCam.SetActive(true);
            if (Menus.isGameStopped)
            {
                thirdPersonCamCrosshair.SetActive(false);
            }
            else
            {
                thirdPersonCamCrosshair.SetActive(true);
            }
        }
    }
}
