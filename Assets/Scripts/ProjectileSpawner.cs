using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private Transform projectilePrefab;
    [SerializeField]
    private float projectileSpeed = 15f;
    private Transform projectileTransform;

    private float spawnTimer;
    private float spawnTimerMax = 1f;


    private void Update()
    {
        Spawn();
        ProjectileMovement();
    }

    private void Spawn()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTimerMax )
        {
            spawnTimer = 0f;
            projectileTransform = Instantiate(projectilePrefab);

            projectileTransform.SetParent(spawnPoint);
        }
    }

    private void ProjectileMovement()
    {
        if (projectileTransform == null) return;

        Vector2 moveVector = new Vector2(0f, 0f);

        moveVector.y = 1f;
        Vector3 moveDir = new Vector3(0f, 0f, moveVector.y);

        float moveDistance = projectileSpeed * Time.deltaTime;
        projectileTransform.position += moveDistance * moveDir;
    }
}
