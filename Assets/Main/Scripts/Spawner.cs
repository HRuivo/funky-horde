using System;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    public float minSpawnInterval = 30f;
    public float maxSpawnInterval = 60f;
    public float firstSpawnDelay = 1f;

    protected abstract void Spawn();
}
