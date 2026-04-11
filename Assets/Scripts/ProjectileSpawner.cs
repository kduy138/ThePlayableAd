using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private Transform projectilePrefab;

    private float spawnTimer;
    private float spawnTimerMax = 0.8f;

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
            Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
