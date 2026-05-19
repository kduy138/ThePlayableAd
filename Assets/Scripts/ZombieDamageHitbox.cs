using UnityEngine;

public class ZombieDamageHitbox : MonoBehaviour
{
    private Zombie zombie;

    private void Awake()
    {
        zombie = GetComponentInParent<Zombie>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            zombie.DealDamage(damagable);
        }
    }
}
