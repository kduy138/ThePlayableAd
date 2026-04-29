using TMPro;
using UnityEngine;

public class SurvivalistBanner : MonoBehaviour, IDamagable
{
    [Header("References")]
    [SerializeField]
    private TextMeshPro survivalistBannerTxt;

    [Header("Settings")]
    private float moveSpeed = 12f;
    private float bannerMaxHP = 1000f;
    private float bannerCurrentHP;
    private int survivalistAmount;
    private int minAmount = 0;
    private int maxAmount = 5;

    private void Awake()
    {
        bannerCurrentHP = bannerMaxHP;
    }

    private void Start()
    {
        survivalistAmount = RandomizeSurvivalistAmount();
        survivalistBannerTxt.text = survivalistAmount >= 0 ? "+" + survivalistAmount.ToString() : survivalistAmount.ToString();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 moveDir = Vector3.back;
        float moveDistance = moveSpeed * Time.deltaTime;

        transform.position += moveDistance * moveDir;
    }

    public void TakeDamage(float damage)
    {
        bannerCurrentHP -= damage;

        if (bannerCurrentHP <= 0)
        {
            ReturnSelfToPool();
            bannerCurrentHP = bannerMaxHP;
        }
    }

    public void ReturnSelfToPool()
    {
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }

    private int RandomizeSurvivalistAmount()
    {
        int randomAmount = Random.Range(minAmount, maxAmount + 1);

        return randomAmount;
    }

    public int GetSurvivalistAmount()
    {
        return survivalistAmount;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Survivalist survivalist))
        {
            Debug.Log("Banner Collides!");
        }
    }
}
