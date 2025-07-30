using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnData
{
    public GameObject EnemyToSpawn;
    public float TimeBeforeSpawn;
    public Transform SpawnPoint;
    public Transform EndPoint;
}

[System.Serializable]
public struct WaveData
{
    public float timeBeforeWave;
    public List<SpawnData> enemyData;
}
public class WaveManager : MonoBehaviour
{
    public List<WaveData> LevelWaveData;

    void Start()
    {
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel()
    {
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        foreach (WaveData currentWave in LevelWaveData)
        {
            yield return new WaitForSeconds(currentWave.timeBeforeWave);
            foreach (SpawnData currentEnemyToSpawn in currentWave.enemyData)
            {
                yield return new WaitForSeconds(currentEnemyToSpawn.TimeBeforeSpawn);
                SpawnEnemy(currentEnemyToSpawn.EnemyToSpawn, currentEnemyToSpawn.SpawnPoint, currentEnemyToSpawn.EndPoint);
            }
        }
    }

    public void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint, Transform endPoint)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab,spawnPoint.position,spawnPoint.rotation);
        enemyInstance.GetComponent<Enemy>().Initialize(endPoint);
    }
}
