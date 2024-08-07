using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{

    [Header("Rifle Components")]
    public Camera cam;
    public float rifleDamage = 10f;
    public float shootingRange = 10f;
    public float fireRate = 10f;
    private float nextShotTimer = 0;

    [Header("Rifle Ammunations")]
    public int magHolsterSize = 100;
    private int magsRemaining;
    public int magSize = 30;
    public float reloadTimer = 2.0f;
    private int ammoInCurrentMag ;
    private bool isReloading = false;
    

    [Header("Rifle Effects")]
    public ParticleSystem muzzleSpark;
    public GameObject woodHitEffect;
    public GameObject bloodEffect;

    [Header("Misc")]
    public PlayerMovementScript player;
    public Transform playerHand;
    public Animator animator;
    public GameObject ammoUI;

    // Start is called before the first frame update
    void Awake()
    {
        ammoUI.SetActive(true);
        transform.SetParent(playerHand);
        ammoInCurrentMag = magSize;

        magsRemaining = magHolsterSize;

        AmmoUI.instance.UpdateAmmoText(ammoInCurrentMag);
        AmmoUI.instance.UpdateMagText(magsRemaining);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReloading)
        {

            if (nextShotTimer >= -50f)
            {
                nextShotTimer -= Time.deltaTime;
            }

            if (Input.GetButton("Fire1") && nextShotTimer <= 0f)
            {
                player.isAiming = true;
                animator.SetBool("Fire", true);
                animator.SetBool("Idle", false);
                nextShotTimer = 1f / fireRate;
                Shoot();
            }
            else if(Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                player.isAiming = true;
                animator.SetBool("Idle", false);
                animator.SetBool("FireWalk", true);
            }
            else if(Input.GetButton("Fire2") && Input.GetButton("Fire1"))
            {
                player.isAiming = true;
                animator.SetBool("Idle", false);
                animator.SetBool("IdleAim", true);
                animator.SetBool("FireWalk", true);
                animator.SetBool("Walk", true);
                animator.SetBool("Reloading", false);
            }
            else
            {
                if (!Input.GetButton("Fire1"))
                {
                    player.isAiming = false;
                    animator.SetBool("Fire", false);
                    animator.SetBool("FireWalk", false);

                }
                animator.SetBool("Idle", true);
            }
        }
    }

    private void Shoot()
    {
        if(magsRemaining == 0)
        {
            // NO AMMO left
            return;
        }

        ammoInCurrentMag--;

        

        //ui  update
        AmmoUI.instance.UpdateAmmoText(ammoInCurrentMag);



        muzzleSpark.Play();
        RaycastHit hitInfo;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);



            Zombie1 zombie1 = hitInfo.transform.GetComponent<Zombie1>();

            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();

            DamagableObject damagableObject = hitInfo.transform.GetComponent<DamagableObject>();

            if(damagableObject != null)
            {
                GameObject woodHitEffectClone = Instantiate(woodHitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                damagableObject.HitDamage(rifleDamage);
                Destroy(woodHitEffectClone,1f);
            }
            else if(zombie1 != null)
            {
                zombie1.ZombieDamaged(rifleDamage);
                GameObject bloodEffectClone = Instantiate(bloodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(bloodEffectClone,1f);
            }
            else if (zombie2 != null)
            {
                zombie2.ZombieDamaged(rifleDamage);
                GameObject bloodEffectClone = Instantiate(bloodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(bloodEffectClone, 1f);
            }

        }
        if (ammoInCurrentMag == 0)
        {
            Debug.Log("0 Ammo");
            StartCoroutine(Reload());
            magsRemaining--;
            AmmoUI.instance.UpdateMagText(magsRemaining);
        }
    }

    IEnumerator Reload()
    {
        player.playerSpeed = 0f;
        player.playerSprint = 0f;

        isReloading = true;
        Debug.Log("Reloading");

        //play sound


        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTimer);

        animator.SetBool("Reloading", false);
        ammoInCurrentMag = magSize;
        isReloading = false;
        nextShotTimer = 0;

        player.playerSpeed = 1.5f;
        player.playerSprint = 3.0f;
    }
}
