using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSounds : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Sound Clips")]
    public AudioClip footStepSound;
    public AudioClip hurtSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hurt()
    {
        audioSource.PlayOneShot(hurtSound);
    }

    public void Step()
    {
        audioSource.PlayOneShot(footStepSound);
    }
}
