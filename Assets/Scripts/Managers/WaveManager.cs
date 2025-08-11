using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private bool lastWave = false;
    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();


    private void Update()
    {
        if(lastWave)
        {
            RemoveDestroyedEnemies();
            if (!spawnedEnemies.Any())
            {
                GameManager.Instance.GameWon();
            }
        }
    }

    void Start()
    {
        StartLevel();
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
                GameManager.Instance.PlaySound();
            }
        }
        lastWave = true;
    }

    public void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint, Transform endPoint)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab,spawnPoint.position,spawnPoint.rotation);
        enemyInstance.GetComponent<Enemy>().Initialize(endPoint);
        spawnedEnemies.Add(enemyInstance);
    }

    private void RemoveDestroyedEnemies()
    {
        for (int i = 0; i < spawnedEnemies.Count; ++i)
        {
            if (spawnedEnemies[i] == null)
            {
                spawnedEnemies.RemoveAt(i);
            }
        }
    }
}
