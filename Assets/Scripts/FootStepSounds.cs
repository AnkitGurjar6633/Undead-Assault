using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSounds : MonoBehaviour
{
    AudioSource audioSource;

    [Header("Sound Clips")]
    public AudioClip[] footStepSounds;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    AudioClip GetRandomClip()
    {
        return footStepSounds[Random.Range(0, footStepSounds.Length)];
    }

    public void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }
}
