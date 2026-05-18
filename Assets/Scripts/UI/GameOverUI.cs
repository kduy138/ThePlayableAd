using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Button tryAgainBtn;
    [SerializeField]
    private Button backToMenuBtn;
    [SerializeField]
    private TextMeshProUGUI timeSurvivedTxt;
    [SerializeField]
    private TextMeshProUGUI totalZombieKilledTxt;

    private void Awake()
    {
        tryAgainBtn.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
        });

        backToMenuBtn.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MenuScene);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void OnEnable()
    {
        SetUI();
    }

    private void SetUI()
    {
        Debug.Log(GameManager.Instance.GetTotalZombieKilled());
        totalZombieKilledTxt.text = "Total zombie killed: " + GameManager.Instance.GetTotalZombieKilled().ToString();

        float minute = GameManager.Instance.GetMinutePlayed();
        float second = GameManager.Instance.GetSecondPlayed();

        string secondTxt = second < 10 ? "0" + second.ToString() : second.ToString();
        string minTxt = minute < 10 ? "0" + minute : minute.ToString();

        timeSurvivedTxt.text = "Time survived: " + minTxt + ":" + secondTxt;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
