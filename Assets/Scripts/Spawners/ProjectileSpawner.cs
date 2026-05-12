using UnityEngine;

public class ProjectileSpawner : BaseSpawner
{
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private Transform projectilePrefab;

    private float spawnTimer;
    private float spawnTimerMax = 0.3f;

    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        SpawnObject();
    }

    public override void SpawnObject()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTimerMax )
        {
            spawnTimer = 0f;
            ObjectPoolManager.SpawnObject(projectilePrefab.gameObject, spawnPoint.position, Quaternion.identity, ObjectPoolManager.PoolType.Bullets);
        }
    }
}
