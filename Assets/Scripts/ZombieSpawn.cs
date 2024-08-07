
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    [Header("Zombie Spawn Components")]
    public GameObject[] zombiePrefabs;
    public Transform[] spawnPositions;
    float spawnCycle = 15f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            InvokeRepeating("SpawnEnemy", 1f, spawnCycle);
            Destroy(gameObject, 150f);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
    private void SpawnEnemy()
    {
        int prefabIndex = Random.Range(0, zombiePrefabs.Length);
        int spawnIndex = Random.Range(0, spawnPositions.Length);
        GameObject zombieClone = Instantiate(zombiePrefabs[prefabIndex], spawnPositions[spawnIndex].position, spawnPositions[spawnIndex].rotation);
        zombieClone.SetActive(true);
    }

}
