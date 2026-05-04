using TMPro;
using UnityEngine;

public class SurvivalistBanner : MonoBehaviour, IDamagable
{
    [Header("References")]
    [SerializeField]
    private TextMeshPro survivalistBannerTxt;
    private Survivalist survivalistGroup;

    [Header("Settings")]
    private float moveSpeed = 12f;
    private float bannerMaxHP = 1000f;
    private float bannerCurrentHP;
    private int survivalistAmount;
    private int minAmount = -5;
    private int maxAmount = 5;
    private float destroyDistance = 30f;

    private void Awake()
    {
        survivalistGroup = FindAnyObjectByType<Survivalist>();
        bannerCurrentHP = bannerMaxHP;
        survivalistAmount = RandomizeSurvivalistAmount();
    }

    private void Update()
    {
        HandleMovement();
        survivalistBannerTxt.text = survivalistAmount >= 0 ? "+" + survivalistAmount.ToString() : survivalistAmount.ToString();

        if (transform.position.z < survivalistGroup.transform.position.z - destroyDistance)
        {
            DestroySelf();
        }
    }

    private void HandleMovement()
    {
        Vector3 moveDir = Vector3.back;
        float moveDistance = moveSpeed * Time.deltaTime;
        transform.position += moveDistance * moveDir;
    }

    public void TakeDamage(float damage) { }

    public void DestroySelf()
    {
        Destroy(gameObject);
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
}
