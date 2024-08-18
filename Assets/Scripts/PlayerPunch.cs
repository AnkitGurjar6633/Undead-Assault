using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [Header("Player Punch Values")]
    public float punchDamage = 5.0f;
    private float punchRange = 1.5f;
    //private float punchRate = .9f;
    //public float nextPunchTimer = 0f;
    private bool isRifleEquipped = false;

    [Header("References")]
    public GameObject player;
    public PlayerMovementScript playerMovementScript;
    public Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isRifleEquipped)
        {
            //if (nextPunchTimer >= -10)
            //{
            //    nextPunchTimer -= Time.deltaTime;
            //}
            if (Input.GetButtonDown("Fire1"))
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Running", false);
                animator.SetTrigger("Punch");
                playerMovementScript.canMove = false;
                playerMovementScript.canJump = false;
                //nextPunchTimer = 1f / punchRate;
                //Punch();
            }
            //else
            //{
            //    animator.SetBool("Punch", false);
            //    animator.SetBool("Idle", true);
            //}
        }
        
    }

    public void PunchReset()
    {
        animator.ResetTrigger("Punch");
        animator.SetBool("Idle", true);
        playerMovementScript.canMove = true;
        playerMovementScript.canJump = true;
    }

    public void Punch()
    {
        RaycastHit hitInfo;


        if (Physics.Raycast(player.transform.position, player.transform.forward, out hitInfo, punchRange))
        {
            Debug.Log(hitInfo.transform.name);

            Zombie1 zombie1 = hitInfo.transform.GetComponent<Zombie1>();

            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();

            DamagableObject damagableObject = hitInfo.transform.GetComponent<DamagableObject>();

            if (damagableObject != null)
            {
                damagableObject.HitDamage(punchDamage);
            }

            else if (zombie1 != null)
            {
                zombie1.ZombieDamaged(punchDamage);
            }
            else if (zombie2 != null)
            {
                zombie2.ZombieDamaged(punchDamage);
            }
        }
    }
}
