using System.Collections.Generic;
using UnityEngine;

public class Survivalist : MonoBehaviour, IDamagable
{
    [Header("Settings")]
    private float moveSpeed = 10f;
    private bool isMoving = false;
    private int maxNumberOfSurvivalists = 49; // 7 survivalists per row and i want 7 rows max => 49
    private float spacing = 1.5f;
    private float takeDamageTimer = 0.5f;
    private float lastTakenDamageTime = -1f;

    [Header("References")]
    [SerializeField]
    private Transform survivalistGroup;
    [SerializeField]
    private Transform survivalistPrefab;
    [SerializeField]
    private List<Transform> survivalists = new List<Transform>();
    private BoxCollider boxCollider;
    [SerializeField]
    private Transform firstSurvivalist;
    [SerializeField]
    private LayerMask damagableLayerMask;

    [Header("Flags")]
    private bool allSurvivalistsAreDead;

    private void Awake()
    {
        allSurvivalistsAreDead = false;
        boxCollider = GetComponent<BoxCollider>();
        survivalists.Add(firstSurvivalist);
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        if (survivalists.Count <= 0) return;

        UpdateSurvivalistsFormation();
        UpdateSurvivalistGroupColliderSize();

        if (allSurvivalistsAreDead)
        {
            GameManager.Instance.StopGameTime();
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, 0f);

        float moveDistance = moveSpeed * Time.fixedDeltaTime;

       BoxCollider col = GetComponent<BoxCollider>();
        bool canMove = !Physics.BoxCast(transform.position, col.size / 2, moveDir, Quaternion.identity, moveDistance, damagableLayerMask);

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isMoving = canMove && moveDir != Vector3.zero;
    }

    private void AddOrRemoveSurivalist(int amount)
    {
        if (amount == 0) return;

        if (amount > 0)
        {
            if (survivalists.Count + amount > maxNumberOfSurvivalists)
            {
                Debug.Log("You've reached the max amount of survivalist! Cannot add more!");
                return;
            }

            for (int i = 0; i < amount; i++)
            {
                Transform survivalist = Instantiate(survivalistPrefab, transform);
                survivalists.Add(survivalist);
            }
        }
        else
        {
            amount = amount * -1;
            if (amount >= survivalists.Count)
            {
                foreach (Transform child in survivalistGroup)
                {
                    Destroy(child.gameObject);
                }
                survivalists.Clear();
                SetAllSurvalistsAreDead();
            }
            else
            {
                for (int i = 0; i < amount; i++)
                {
                    int removeIdx = survivalists.Count - 1;
                    Destroy(survivalists[removeIdx].gameObject);
                    survivalists.RemoveAt(removeIdx);
                }
            }
        }
    }

    private void SetAllSurvalistsAreDead()
    { 
        allSurvivalistsAreDead = true;
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
                int survivalistAmount = banner.GetSurvivalistAmount();
                AddOrRemoveSurivalist(survivalistAmount);
                banner.DestroySelf();
            }
        }
    }

    private void UpdateSurvivalistGroupColliderSize()
    {
        if (boxCollider == null) return;
        if (survivalists.Count <= 0) return;

        float addtionalSpacing = 0.5f;
        float maxColliderSizeX = 10.5f;

        Vector3 size = boxCollider.size;
        size.x = survivalists.Count + survivalists.Count * addtionalSpacing;

        if (size.x > maxColliderSizeX)
        {
            size.x = maxColliderSizeX;
        }

        boxCollider.size = size;
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public bool AllSurvivalistsAreDead()
    {
        return allSurvivalistsAreDead;
    }

    public void TakeDamage(float damage)
    {
        if (survivalists.Count <= 0) return;

        if (Time.time - lastTakenDamageTime > takeDamageTimer)
        {
            return;
        }
        lastTakenDamageTime = Time.time;
        int survivalistKilledAmount = -1;
        AddOrRemoveSurivalist(survivalistKilledAmount);
    }
}
