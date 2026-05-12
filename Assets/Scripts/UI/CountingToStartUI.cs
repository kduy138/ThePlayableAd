using TMPro;
using UnityEngine;

public class CountingToStartUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI countDownTxt;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Show();
    }

    private void Update()
    {
        countDownTxt.text = Mathf.Ceil(GameManager.Instance.GetCountDownToStartTimer()).ToString();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.isCountingToStart())
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
