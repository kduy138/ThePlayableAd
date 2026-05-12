using Unity.Burst;
using UnityEngine;

public class Zombie : MonoBehaviour, IDamagable
{
    [Header("References")]
    private Survivalist survivalistGroup;

    [Header("Settings")]
    private float moveSpeed = 6f;
    private float rotateSpeed = 10f;
    [SerializeField]
    private float maxHP = 100f;
    private float currentHP;

    private void Awake()
    {
        survivalistGroup = FindAnyObjectByType<Survivalist>();
        currentHP = maxHP;
    }

    private void Start()
    {
        SetTarget(survivalistGroup);
    }

    public void CustomUpdate()
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        if (survivalistGroup == null) return;
        HandleMovement();
    }

    private void OnEnable()
    {
        ZombieManager.zombies.Add(this);
    }

    private void OnDisable()
    {
        ZombieManager.zombies.Remove(this);
    }

    private void HandleMovement()
    {
        if (transform.position.z <= survivalistGroup.transform.position.z) return;

        Vector3 moveDir = Vector3.back;
        float moveDistance = moveSpeed * Time.deltaTime;
        transform.position += moveDistance * moveDir;

        Quaternion targetRotation = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    public void SetTarget(Survivalist target)
    {
        survivalistGroup = target;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}
