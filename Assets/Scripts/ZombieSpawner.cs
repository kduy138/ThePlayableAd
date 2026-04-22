using Unity.Burst;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform zombiePrefab;

    private float spawnTimer;
    private float spawnTimerMax = 1.5f;

    private void Update()
    {
        SpawnZombie();
    }

    private void SpawnZombie()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTimerMax)
        {
            spawnTimer = 0f;

            ObjectPoolManager.SpawnObject(zombiePrefab.gameObject, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Zombies);
        }
    }
}
