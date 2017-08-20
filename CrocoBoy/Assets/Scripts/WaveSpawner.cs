using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public Transform enemy;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 6f;
    private float waveCountdown;

    public int enemiesPerWave = 3;
    public float timeBetweenEnemies = 1f;

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referrenced.");
        }

        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {

        if (waveCountdown <= 0)
        {
            StartCoroutine(SpawnWave());
            //wait the timeBetweenWaves plus all the time wasted generating enemies
            waveCountdown = timeBetweenWaves + (enemiesPerWave * timeBetweenEnemies);
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave()
    {

        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy(enemy);
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, randomSpawnPoint.position, randomSpawnPoint.rotation);
    }
}
