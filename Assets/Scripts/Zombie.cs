using Unity.Burst;
using UnityEngine;

[BurstCompile]
public class Zombie : MonoBehaviour
{
    private Survivalist survivalistGroup;
    private float moveSpeed = 6f;
    private float rotateSpeed = 10f;

    private float disableDistance = 70f;
    private Collider collider;

    private void Awake()
    {
        survivalistGroup = FindAnyObjectByType<Survivalist>();
        collider = GetComponent<Collider>();
    }

    [BurstCompile]
    private void Start()
    {
        SetTarget(survivalistGroup);
    }

    [BurstCompile]
    private void Update()
    {
        HandleMovement();

        if (survivalistGroup == null) return;

        float distance = Vector3.Distance(transform.position, survivalistGroup.transform.position);

        collider.enabled = distance < disableDistance;
    }

    [BurstCompile]
    private void HandleMovement()
    {
        if (survivalistGroup == null) return;

        Vector3 moveDir = (survivalistGroup.transform.position - transform.position).normalized;
        float moveDistance = moveSpeed * Time.deltaTime;
        transform.position += moveDistance * moveDir;

        Quaternion targetRotation = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    public void SetTarget(Survivalist target)
    {
        survivalistGroup = target;
    }
}
