
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    [Header("Zombie Spawn Components")]
    public GameObject[] zombiePrefabs;
    public Transform[] spawnPositions;
    public GameObject dangerZoneUI;
    float spawnCycle = 15f;

    [Header("Sounds")]
    public AudioClip dangerZoneSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(dangerZoneSound);
            StartCoroutine(ShowDangerZoneUI());
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

    IEnumerator ShowDangerZoneUI()
    {
        dangerZoneUI.SetActive(true);
        yield return new WaitForSeconds(5f);
        dangerZoneUI.SetActive(false);
    }
}
