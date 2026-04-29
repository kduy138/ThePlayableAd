using System.Collections.Generic;
using UnityEngine;

public class Survivalist : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float moveSpeed = 10f;
    private bool isMoving = false;
    private int maxNumberOfSurvivalists = 50;
    private int currentNumberOfSurvivalists = 1;
    private float spacing = 1.5f;

    [Header("References")]
    [SerializeField]
    private Transform survivalistGroup;
    [SerializeField]
    private Transform survivalistPrefab;
    private List<Transform> survivalists = new List<Transform>();
    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        HandleMovement();
        UpdateSurvivalistsFormation();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, 0f);

        float moveDistance = moveSpeed * Time.deltaTime;

        transform.position += moveDistance * moveDir;
        isMoving = moveDir != Vector3.zero;
    }

    private void AddSurivalist(int amount)
    {
        if (amount <= 0) return;
        if (currentNumberOfSurvivalists + amount > maxNumberOfSurvivalists)
        {
            Debug.Log("You've reached the max amount of survivalist! Cannot add more!");
        }

        for (int i = 0; i < amount; i++)
        {
            Transform survivalist = Instantiate(survivalistPrefab);
            survivalists.Add(survivalist);
            currentNumberOfSurvivalists++;
        }
    }

    private void UpdateSurvivalistsFormation()
    {
        for (int i = 0; i < survivalists.Count; i++)
        {
            Vector3 pos = GetFormationPos(i);
            survivalists[i].position = Vector3.Lerp(survivalists[i].position, transform.position + pos, Time.deltaTime * moveSpeed);
            survivalists[i].SetParent(transform);
        }
    }

    private Vector3 GetFormationPos(int index)
    {
        int row = index / 7;
        int col = index % 7;

        Vector3 offset = new Vector3((col - 1) * spacing, 0, -(row + 1) * spacing); 

        return offset;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out SurvivalistBanner banner))
        {
            if (banner != null)
            {
                Debug.Log("Collided");
                int survivalistAmount = banner.GetSurvivalistAmount();
                AddSurivalist(survivalistAmount);
                UpdateSurvivalistGroupColliderSize(survivalistAmount);
                banner.ReturnSelfToPool();
            }
        }
    }

    private void UpdateSurvivalistGroupColliderSize(int survivalistAmount)
    {
        if (boxCollider == null) return;
        if (survivalistAmount <= 0) return;

        Vector3 size = boxCollider.size;
        size.x += survivalistAmount;
        boxCollider.size = size;
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
