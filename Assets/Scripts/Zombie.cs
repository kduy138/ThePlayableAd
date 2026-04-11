using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField]
    private Survivalist survivalistGroup;
    private float moveSpeed = 6f;

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
    }

    public void SetTarget(Survivalist target)
    {
        survivalistGroup = target;
    }
}
