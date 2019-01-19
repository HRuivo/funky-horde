using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public PoolManager projectilePool;
    public PoolManager healthItemPool;
    public PoolManager ammoItemPool;

    public PoolManager enemySlimePool;
    public PoolManager enemyDragonPool;
    public PoolManager enemySkeletonPool;

    public float restartingDelay = 1f;

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

        Init();
    }

    void Init()
    {
        Player = FindObjectOfType<Player>();
    }

    public void Restart()
    {
        StartCoroutine("ReloadScene", restartingDelay);
    }

    IEnumerator ReloadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void DespawnEnemy(Enemy.EEnemyType type, Transform instance)
    {
        switch (type)
        {
            case Enemy.EEnemyType.Slime:
                enemySlimePool.Despawn(instance);
                break;

            case Enemy.EEnemyType.Bat:
                break;

            case Enemy.EEnemyType.Dragon:
                enemyDragonPool.Despawn(instance);
                break;

            case Enemy.EEnemyType.Skeleton:
                enemySkeletonPool.Despawn(instance);
                break;

            case Enemy.EEnemyType.Ghost:
            default:
                break;
        }
    }

    public static GameManager Instance
    {
        get;
        private set;
    }

    public Player Player
    {
        get;
        private set;
    }
}
