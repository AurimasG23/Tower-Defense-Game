using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] enemyPrefabs;

    float timeBetweenWaves = 3f;
    float countDown = 2f;
    int waveNumber = 0;

    public Transform spawnPoint;

    public GameObject spawnPointContainer;
    public Transform[] spawnPoints;
    int spawnPointsCount;

    private void Start()
    {
        spawnPointsCount = spawnPointContainer.transform.childCount;
        spawnPoints = new Transform[spawnPointsCount];
        for (int i = 0; i < spawnPointsCount; i++)
        {
            spawnPoints[i] = spawnPointContainer.transform.GetChild(i);
        }
    }


    private void Update()
    {
        if (GamePlayManager.instance.GetLiveCount() > 0 && !GamePlayManager.instance.IsGamePaused())
        {
            if (countDown <= 0f)
            {
                StartCoroutine(SpawnWave());
                countDown = timeBetweenWaves;
            }

            countDown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave()
    {
        waveNumber++;
        int spawnedEnemyCount = 0;
        if(waveNumber < 5)
        {
            spawnedEnemyCount = Random.Range(waveNumber + 1, 6); // 2 - 6
        }
        else if(waveNumber >= 5 && waveNumber < 10)
        {
            spawnedEnemyCount = waveNumber + Random.Range(0, waveNumber / 2); // 5 - 13
        }
        else
        {
            spawnedEnemyCount = 10 + Random.Range(0, waveNumber / 5); // 10 - ...
        }

        for (int i = 0; i < spawnedEnemyCount; i++)
        {
            if (GamePlayManager.instance.GetLiveCount() > 0 && !GamePlayManager.instance.IsGamePaused())
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(0.2f);
        }        
    }

    void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        int point = Random.Range(0, spawnPointsCount);
        Instantiate(enemyPrefabs[index], spawnPoints[point].position, spawnPoint.rotation);
    }
}
