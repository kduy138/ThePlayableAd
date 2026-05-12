using System;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [Header("Settings")]
    private float projectileSpeed = 30f;
    private float projectileDamage = 100f;
    private float projectileLifeTimer;
    private float projectileLifeTimerMax = 3f;
    [SerializeField]
    private LayerMask damagableLayer;

    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        float moveDistance = projectileSpeed * Time.deltaTime;

        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, moveDistance, damagableLayer))
        {
            if (hit.collider.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(projectileDamage);
            }

            ObjectPoolManager.ReturnObjectToPool(gameObject);
            projectileLifeTimer = 0f;
            return;
        }

        transform.position += transform.forward * moveDistance;

        projectileLifeTimer += Time.deltaTime;
        if (projectileLifeTimer > projectileLifeTimerMax)
        {
            projectileLifeTimer = 0f;
            ObjectPoolManager.ReturnObjectToPool(transform.gameObject);
        }
    }
}
