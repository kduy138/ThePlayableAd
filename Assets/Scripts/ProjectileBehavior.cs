using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private float projectileSpeed = 30f;
    private float projectileLifeTimer;
    private float projectileLifeTimerMax = 3f;

    private void Update()
    {
        ProjectileMovement();

        projectileLifeTimer += Time.deltaTime;
        if (projectileLifeTimer > projectileLifeTimerMax)
        {
            projectileLifeTimer = 0f;
            DestroySelf();
        }
    }

    private void ProjectileMovement()
    {
        Vector2 moveVector = new Vector2(0f, 0f);

        moveVector.y = 1f;
        Vector3 moveDir = new Vector3(0f, 0f, moveVector.y);

        float moveDistance = projectileSpeed * Time.deltaTime;
        transform.position += moveDistance * moveDir;
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
