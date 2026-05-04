using System.Collections.Generic;
using UnityEngine;

public class Survivalist : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float moveSpeed = 10f;
    private bool isMoving = false;
    private int maxNumberOfSurvivalists = 50;
    [SerializeField]
    private int currentNumberOfSurvivalists = 1;
    private float spacing = 1.5f;

    [Header("References")]
    [SerializeField]
    private Transform survivalistGroup;
    [SerializeField]
    private Transform survivalistPrefab;
    private List<Transform> survivalists = new List<Transform>();
    private BoxCollider boxCollider;
    [SerializeField]
    private Transform firstSurvivalist;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        survivalists.Add(firstSurvivalist);
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

    private void AddOrRemoveSurivalist(int amount)
    {
        if (amount == 0) return;

        if (currentNumberOfSurvivalists + amount > maxNumberOfSurvivalists)
        {
            Debug.Log("You've reached the max amount of survivalist! Cannot add more!");
        }

        UpdateSurvivalistGroupColliderSize(amount);

        if (amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                Transform survivalist = Instantiate(survivalistPrefab, transform);
                survivalists.Add(survivalist);
                currentNumberOfSurvivalists++;
            }
        }
        else
        {
            amount = amount * -1;
            for (int i = 0; i < amount; i++)
            {
                Destroy(survivalists[i].gameObject);
                survivalists.Remove(survivalists[i]);
                currentNumberOfSurvivalists--;
            }

            if (currentNumberOfSurvivalists <= 0)
            {
                // Player loses the game
                Debug.Log("All survivalists are dead!");
                Time.timeScale = 0f;
            }
        }

    }

    private void UpdateSurvivalistsFormation()
    {
        for (int i = 0; i < survivalists.Count; i++)
        {
            Vector3 pos = GetInFormationPosForObject(i);
            survivalists[i].position = Vector3.Lerp(survivalists[i].position, transform.position + pos, Time.deltaTime * moveSpeed);
        }
    }

    private Vector3 GetInFormationPosForObject(int index)
    {
        int maxRow = 7;
        int maxCol = 7;
        int row = index / maxRow;
        int col = index % maxCol;

        int colCount = Mathf.Min(survivalists.Count, maxCol);
        float centerOffset = (colCount - 1) * 0.5f;

        Vector3 offset = new Vector3((col - centerOffset) * spacing, 0, -(row + 1) * spacing); 

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
                AddOrRemoveSurivalist(survivalistAmount);
                banner.DestroySelf();
            }
        }
    }

    private void UpdateSurvivalistGroupColliderSize(int amount)
    {
        float maxColliderSizeX = 7 + 7 * spacing; // Because the maximum survivalist per row is 7
        if (boxCollider == null) return;
        if (amount == 0) return;

        Vector3 size = boxCollider.size;

        if (amount > 0)
        {
            size.x += amount + spacing;
            if (size.x > maxColliderSizeX)
            {
                size.x = maxColliderSizeX;
            }
        }
        else
        {
            size.x -= (amount * -1) + ((amount * -1) * spacing);
        }
        
        boxCollider.size = size;
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
