using UnityEngine;

public class Survivalist : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    private bool isMoving = false;

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, 0f);

        float moveDistance = moveSpeed * Time.deltaTime;

        transform.position += moveDistance * moveDir;
        isMoving = moveDir != Vector3.zero;
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
