using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidKit : MonoBehaviour
{
    [Header("FirstAidKit")]
    public PlayerMovementScript player;
    private float healthRestoreAmount = 200f;
    private float rangeRadius = 1.5f;

    [Header("Sounds")]
    public AudioClip firstAidKitSound;
    public AudioSource audioSource;

    [Header("Animator")]
    public Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,player.transform.position) < rangeRadius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                animator.SetBool("Open", true);
                player.currentHealth = healthRestoreAmount;

                player.healthBar.SetHealth(player.currentHealth);

                audioSource.PlayOneShot(firstAidKitSound);

                Destroy(gameObject, 2f);
            }
        }
    }
}
