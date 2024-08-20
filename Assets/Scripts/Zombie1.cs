using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie1 : MonoBehaviour
{
    [Header("Health And Damage")]
    public float maxHealth = 100.0f;
    float currentHealth;
    public float attackDamage = 5f;
    public HealthBar healthBar;

    [Header("Zombie Componenets")]
    public Transform lookPoint;
    public LayerMask playerLayer;
    public Camera AttackingRaycastArea;
    public Transform player;
    public NavMeshAgent zombieAgent;

    [Header("Zombie Idle")]
    public GameObject[] walkPoints;
    int currentZombiePosition =0;
    public float zombieSpeed;
    float walkingPointRadius = 2;

    [Header("Zombie Animations")]
    public Animator animator;

    //[Header("Zombie Attacking")]
    //public float timeBtwAttack;
    //bool previousAttack;

    [Header("Zombie States")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerInVisionRadius;
    public bool playerInAttackingRadius;

    Vector3 bound;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetFullHealth(maxHealth);
        zombieAgent = GetComponent<NavMeshAgent>();
        bound = new Vector3(0, transform.eulerAngles.y, 0);
    }



    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("Walking", true);
        animator.SetBool("Died", false);
        animator.SetBool("Running", false);
        animator.SetBool("Attacking", false);
    }

    // Update is called once per frame
    void Update()
    {
        playerInVisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);
        playerInAttackingRadius = Physics.CheckSphere(transform.position, attackingRadius, playerLayer);

        if(!playerInVisionRadius && !playerInAttackingRadius)
        {
            Idle();
        }
        if(playerInVisionRadius && !playerInAttackingRadius)
        {
            PursuePlayer();
        }
        if(playerInAttackingRadius && playerInAttackingRadius)
        {
            AttackPlayer();
        }
        bound.y = transform.eulerAngles.y;
        transform.eulerAngles = bound;
    }

    void Idle()
    {
        zombieAgent.ResetPath();
        animator.SetBool("Walking", true);
        animator.SetBool("Running", false);
        if (Vector3.Distance(walkPoints[currentZombiePosition].transform.position, transform.position) < walkingPointRadius)
        {
            currentZombiePosition = Random.Range(0,walkPoints.Length);
            if(currentZombiePosition >= walkPoints.Length)
            {
                currentZombiePosition = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentZombiePosition].transform.position, zombieSpeed * Time.deltaTime);
        //turn the zomnbie
        transform.LookAt(walkPoints[currentZombiePosition].transform.position);
    }
    void PursuePlayer()
    {
        if (zombieAgent.SetDestination(player.position))
        {
            animator.SetBool("Running", true);
            animator.SetBool("Walking", false);
            animator.SetBool("Attacking", false);
        }
        transform.LookAt(player.position);
    }

    void AttackPlayer()
    {
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(lookPoint);

        animator.SetBool("Attacking", true);
        animator.SetBool("Running", false);
        animator.SetBool("Walking", false);

        //if (!previousAttack)
        //{
        //    RaycastHit hitInfo;
        //    if(Physics.Raycast(AttackingRaycastArea.transform.position, AttackingRaycastArea.transform.forward, out hitInfo, attackingRadius))
        //    {
        //        Debug.Log("aatack");
        //        PlayerMovementScript playerBody = hitInfo.transform.GetComponent<PlayerMovementScript>();

        //        if (playerBody != null)
        //        {
        //            playerBody.PlayerHitDamage(attackDamage);
        //        }

        //    }
        //    previousAttack = true;
        //    Invoke(nameof(ActiveAttacking), timeBtwAttack);
        //}
    }

    public void Attack()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(AttackingRaycastArea.transform.position, AttackingRaycastArea.transform.forward, out hitInfo, attackingRadius))
        {
            Debug.Log("aatack");
            PlayerMovementScript playerBody = hitInfo.transform.GetComponent<PlayerMovementScript>();

            if (playerBody != null)
            {
                playerBody.PlayerHitDamage(attackDamage);
            }

        }
    }

    //private void ActiveAttacking()
    //{
    //    previousAttack = false;
    //}

    public void ZombieDamaged(float damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            
            animator.SetBool("Died", true);

            Die();
        }
    }
    void Die()
    {
        zombieAgent.SetDestination(transform.position);
        zombieSpeed = 0;
        attackingRadius = 0;
        visionRadius = 0;
        playerInAttackingRadius = false;
        playerInVisionRadius = false;
        Object.Destroy(gameObject, 5.0f);
    }
}
