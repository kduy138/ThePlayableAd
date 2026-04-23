using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private Transform projectilePrefab;

    private float spawnTimer;
    private float spawnTimerMax = 0.3f;

    private void Update()
    {
        SpawnProjectile();
    }

    private void SpawnProjectile()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTimerMax )
        {
            spawnTimer = 0f;
            ObjectPoolManager.SpawnObject(projectilePrefab.gameObject, spawnPoint.position, Quaternion.identity, ObjectPoolManager.PoolType.Bullets);
        }
    }
}
