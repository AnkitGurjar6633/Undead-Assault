using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiflePickUp : MonoBehaviour
{
    [Header("Rifles")]
    public GameObject pickUpRifle;
    public GameObject playerRifle;

    [Header("Rifle Interaction")]
    public PlayerMovementScript player;
    private float pickRangeRadius = 2.5f;
    public PlayerPunch playerPunch;

    public GameObject ammoUI;

    // Start is called before the first frame update
    void Awake()
    {
        playerRifle.SetActive(false);
        ammoUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < pickRangeRadius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                playerPunch.enabled = false;
                pickUpRifle.SetActive(false);
                playerRifle.SetActive(true);

                ObjectiveComplete.instance.Objective1Done();
            }  
        }
    }
}
