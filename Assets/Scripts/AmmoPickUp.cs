using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    [Header("AmmoPickUp")]
    public Rifle rifle;
    private int magPickUpAmount = 15;
    private float rangeRadius = 2.5f;

    [Header("Sounds")]
    public AudioClip ammoPickUpSound;
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
        if (Vector3.Distance(transform.position, rifle.transform.position) < rangeRadius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                animator.SetBool("Open", true);
                if(rifle.magsRemaining + magPickUpAmount > rifle.magHolsterSize)
                {
                    rifle.magsRemaining = rifle.magHolsterSize;
                }
                else
                {
                    rifle.magsRemaining += magPickUpAmount;
                }
                AmmoUI.instance.UpdateMagText(rifle.magsRemaining);
                audioSource.PlayOneShot(ammoPickUpSound);

                Destroy(gameObject, 2f);
            }
        }
    }
}
