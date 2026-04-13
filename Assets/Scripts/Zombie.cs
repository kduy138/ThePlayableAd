using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Survivalist survivalistGroup;
    private float moveSpeed = 6f;
    private float rotateSpeed = 10f;

    private void Awake()
    {
        survivalistGroup = FindAnyObjectByType<Survivalist>();
    }

    private void Start()
    {
        SetTarget(survivalistGroup);
    }

    private void Update()
    {
        HandleMovement();
    }

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
