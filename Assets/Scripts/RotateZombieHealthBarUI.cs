using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateZombieHealthBarUI : MonoBehaviour
{
    public Camera mainCamera;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + mainCamera.transform.forward);
    }
}
