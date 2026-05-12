using Unity.Burst;
using UnityEngine;

public class ZombieSpawner : BaseSpawner
{
    [Header("References")]
    [SerializeField]
    private Transform zombiePrefab;

    [Header("Settings")]
    private float spawnTimer;
    private float spawnTimerMax = 1.5f;

    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        SpawnObject();
    }

    public override void SpawnObject()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTimerMax)
        {
            spawnTimer = 0f;

            ObjectPoolManager.SpawnObject(zombiePrefab.gameObject, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Zombies);
        }
    }
}
