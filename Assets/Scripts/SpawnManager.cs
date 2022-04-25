using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject powerUpPrefab;
    private PlayerController playerControllerScript;
    private float spawnRangeX = 0.83f;
    private float spawnPosY = -0.2f;
    private float spawnPowerUpPosY = -1f;
    private float startDelay = 2;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnRandomEnemy", startDelay, 1.5f);
        InvokeRepeating("SpawnRandomPowerUp", startDelay, 15f);
    }

    void SpawnRandomEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnPosY, -1);
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);
        }
    }

    void SpawnRandomPowerUp()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnPowerUpPosY, -1);
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(powerUpPrefab, spawnPos, powerUpPrefab.transform.rotation);
        }
    }
}
