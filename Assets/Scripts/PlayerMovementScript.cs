using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [Header("Health And Damage")]
    private float maxHealth = 200f;
    public float currentHealth;
    public HealthBar healthBar;

    [Header("Player Movement")]
    public bool canMove = true;
    public bool canJump = true;
    public float playerSpeed = 1.5f;
    public float playerSprint = 3.0f;
    public bool isAiming;
    public bool isFiring;

    [Header("Player Camera Movement")]
    public Transform playerCamera;
    public GameObject aimCam;
    

    [Header("Player Animator")]
    public CharacterController characterController;
    public Animator animator;
    public GameObject playerDamageIndicator;


    [Header("Player Gravity, Jumping and Velocity")]
    public float turnCalmTime = 0.1f;
    float turnCalmVelocity;
    float turnCalmVelocity_A;
    float gravity = -9.8f;
    public float jumpRange = 1f;
    Vector3 velocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;

    public GameObject gameOverUI;

    public Rifle rifle;


    //[Header("Player Gravity")]
    // Start is called before the first frame update
    void Start()
    {
        aimCam.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        currentHealth = maxHealth;

        healthBar.SetFullHealth(maxHealth);

        animator.SetBool("Idle", true);
        animator.SetBool("Walk", false);
        animator.SetBool("Running", false);
        animator.SetBool("Fire", false);
        animator.SetBool("IdleAim", false);
        animator.SetBool("Reloading", false);
        animator.SetBool("RifleWalk", false);
        animator.SetBool("FireWalk", false);
        animator.SetBool("WalkReload", false);
    }

    // Update is called once per frame
    void Update()
    {
        

        //isAiming = aimCam.activeInHierarchy;

        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);

        if(onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        if (canMove)
        {
            PlayerMovement();
            if(!isAiming && !isFiring)
            {
                Sprint();
            }
        }
        if (canJump && !isAiming && !isFiring)
        {
            JumpStart();
        }

        HandleRifleState();

        if(isAiming || isFiring)
        {
            HandleTurnWithFiringAiming();
        }
    }

    private void PlayerMovement()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;

        if(direction.magnitude > 0.1f)
        {
            if (!Input.GetButton("Sprint") || isAiming || isFiring || verticalAxis <= 0)
            {
                animator.SetBool("Walk", true);
            }

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);


            if (!isAiming && !isFiring)
            {

                transform.rotation = Quaternion.Euler(0, angle, 0);

            }


            Vector3 moveDirection = Quaternion.Euler(0,targetAngle,0) * Vector3.forward;
            characterController.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);


            //removed due to animation issues instead added a sprint function

            /*if (Input.GetButton("Sprint") && (horizontalAxis != 0 || verticalAxis!=0) && onSurface)
                {
                    animator.SetBool("Running", true);
                    animator.SetBool("Walk", false);
                    characterController.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
                }
                else
                {
                    animator.SetBool("Walk", true);
                    animator.SetBool("Running", false);
                }*/

        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    private void JumpStart()
    {
        if (Input.GetButtonDown("Jump") && onSurface)
        {
            animator.SetBool("Idle", false);
            animator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.ResetTrigger("Jump");
        }
    }

    public void noJump()
    {
        canJump = false;
    }

    public void setJump()
    {
        canJump = true;
    }

    public void noMove()
    {
        canMove = false;
    }
    public void setMove()
    {
        canMove = true;
    }

    public void ReloadEnd()
    {
        rifle.ReloadEnd();
    }

    private void Sprint()
    {
        if (Input.GetButton("Sprint") && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && onSurface)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Running", true);
            float horizontalAxis = Input.GetAxisRaw("Horizontal");
            float verticalAxis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;

            if (direction.magnitude > 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0, angle, 0);

                Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                characterController.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
            }
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    public void PlayerHitDamage(float damage)
    {
        currentHealth -= damage;
        StartCoroutine(ToggleDamageIndicator());

        healthBar.SetHealth(currentHealth);

        if(currentHealth < 0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Destroy(gameObject, 1.0f);
    }

    IEnumerator ToggleDamageIndicator()
    {
        playerDamageIndicator.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        playerDamageIndicator.SetActive(false);
    }





// Rifle Animations

    void HandleRifleState()
    {
        animator.SetBool("Aiming", isAiming);
        animator.SetBool("Firing", isFiring);
        if(isAiming || isFiring)
        {
            animator.SetBool("Running", false);
        }
    }

    public void HandleTurnWithFiringAiming()
    {
        float targetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, playerCamera.eulerAngles.y, ref turnCalmVelocity_A, turnCalmTime);

        transform.rotation = Quaternion.Euler(0, targetAngle, 0);
    }


}