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
    private bool m_lastWave = false;
    [SerializeField] private List<GameObject> m_spawnedEnemies = new List<GameObject>();


    private void Update()
    {
        if(m_lastWave)
        {
            RemoveDestroyedEnemies();
            if (!m_spawnedEnemies.Any())
            {
                GameManager.Instance.GameWon();
            }
        }
    }
    public void StartLevel()
    {
        Time.timeScale = 1f;
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
            GameManager.Instance.IncrementWavesCompleted();
        }
        m_lastWave = true;
    }

    public void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint, Transform endPoint)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab,spawnPoint.position,spawnPoint.rotation);
        enemyInstance.GetComponent<Enemy>().Initialize(endPoint);
        m_spawnedEnemies.Add(enemyInstance);
    }

    private void RemoveDestroyedEnemies()
    {
        for (int i = 0; i < m_spawnedEnemies.Count; ++i)
        {
            if (m_spawnedEnemies[i] == null)
            {
                m_spawnedEnemies.RemoveAt(i);
            }
        }
    }
}
