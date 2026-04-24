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
    private int minAmount = -10;
    private int maxAmount = 5;

    private void Awake()
    {
        bannerCurrentHP = bannerMaxHP;
        survivalistAmount = RandomizeSurvivalistAmount();
    }

    private void Start()
    {
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
            ObjectPoolManager.ReturnObjectToPool(gameObject);
            bannerCurrentHP = bannerMaxHP;
        }
    }

    private int RandomizeSurvivalistAmount()
    {
        int randomAmount = Random.Range(minAmount, maxAmount);

        return randomAmount;
    }
}
