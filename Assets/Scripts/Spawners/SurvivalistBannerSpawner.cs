using UnityEngine;

public class SurvivalistBannerSpawner : BaseSpawner
{
    [Header("References")]
    [SerializeField]
    private Transform survivalistBannerPrefab;

    [Header("Settings")]
    private float spawnTimer;
    private float spawnTimerMax = 5f;

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

            ObjectPoolManager.SpawnObject(survivalistBannerPrefab.gameObject, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Zombies);
        }
    }
}
