using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform zombiePrefab;

    private float spawnTimer;
    private float spawnTimerMax = 1f;

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
            Instantiate(zombiePrefab, transform.position, Quaternion.identity);
        }
    }
}
