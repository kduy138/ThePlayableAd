using Unity.Burst;
using UnityEngine;

public class Zombie : MonoBehaviour, IDamagable
{
    [Header("References")]
    private Survivalist survivalistGroup;
    private float moveDistance;
    private Vector3 moveDir;
    [SerializeField]
    private LayerMask damagableLayerMask;

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
        DealDamage();
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
        this.moveDir = moveDir;

        float moveDistance = moveSpeed * Time.deltaTime;
        this.moveDistance = moveDistance;

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

            int zombieKilled = 1;
            GameManager.Instance.SetTotalZombieKilled(zombieKilled);
        }
    }

    public void DealDamage()
    { 
        BoxCollider col = GetComponent<BoxCollider>();
        float zombieRadius = 0.5f;
        float zombieHeight = 1.92f;

        if (Physics.CapsuleCast(transform.position, transform.position + Vector3.up * zombieHeight, zombieRadius, moveDir, moveDistance ,damagableLayerMask))
        {
            survivalistGroup.TakeDamage(0);
        }
    }
}
