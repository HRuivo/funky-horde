using System;
using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;

public class HorderManager : MonoBehaviour
{
    [Serializable]
    public class Wave
    {
        public Enemy.EEnemyType[] enemies;
    }

    public Wave[] waves;
    public int startingWave = 1;
    public float waveStartDelay = 2f;
    public Transform[] spawnPoints;

    private int _enemiesRemaining = 0;


    public delegate void WaveEventHandler(int currentWave);

    public event WaveEventHandler OnNewWaveStart;
    public event WaveEventHandler OnWaveComplete;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CurrentWave = startingWave;

        StartCoroutine("StartWave", startingWave);
    }

    public void StartNewWave()
    {
        CurrentWave += 1;
        StartCoroutine("StartWave", CurrentWave);
    }

    private IEnumerator StartWave(int waveNumber)
    {
        yield return new WaitForSeconds(waveNumber > 1 ? waveStartDelay : 2f);

        CurrentWave = waveNumber;

        int waveIndex = Mathf.Clamp(waveNumber, 1, waves.Length) - 1;

        Enemy.EEnemyType[] enemyList = waves[waveIndex].enemies;

        for (int i = 0; i < enemyList.Length; i++)
        {
            switch (enemyList[i])
            {
                case Enemy.EEnemyType.Slime:
                    GameManager.Instance.enemySlimePool.Spawn(spawnPoints[Random.Range(0, spawnPoints.Length)].position);
                    break;

                case Enemy.EEnemyType.Bat:
                    break;

                case Enemy.EEnemyType.Ghost:
                    break;

                case Enemy.EEnemyType.Skeleton:
                    GameManager.Instance.enemySkeletonPool.Spawn(spawnPoints[Random.Range(0, spawnPoints.Length)].position);
                    break;

                case Enemy.EEnemyType.Dragon:
                default:
                    GameManager.Instance.enemyDragonPool.Spawn(spawnPoints[Random.Range(0, spawnPoints.Length)].position);
                    break;
            }
        }

        _enemiesRemaining = enemyList.Length;

        if (OnNewWaveStart != null)
            OnNewWaveStart(CurrentWave);
    }

    public void NotifyEnemyKill()
    {
        _enemiesRemaining--;

        if (_enemiesRemaining <= 0)
        {
            if (OnWaveComplete != null)
                OnWaveComplete(CurrentWave);

            StartNewWave();
        }
    }

    public static HorderManager Instance
    {
        get;
        private set;
    }

    public int CurrentWave
    {
        get;
        private set;
    }
}
