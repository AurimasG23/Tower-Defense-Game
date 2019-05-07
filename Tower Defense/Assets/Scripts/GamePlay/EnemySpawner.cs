using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    float timeBetweenWaves = 5f;
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
        if (GamePlayManager.instance.GetLiveCount() > 0)
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

        for (int i = 0; i < waveNumber; i++)
        {
            if (GamePlayManager.instance.GetLiveCount() > 0)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(0.3f);
        }        
    }

    void SpawnEnemy()
    {
        int point = Random.Range(0, spawnPointsCount);
        Instantiate(enemyPrefab, spawnPoints[point].position, spawnPoint.rotation);
    }
}
