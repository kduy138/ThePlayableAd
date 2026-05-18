using TMPro;
using UnityEngine;

public class GamePlayingUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI zombieKilledCountTxt;
    [SerializeField]
    private TextMeshProUGUI gamePlayedTimeTxt;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void Update()
    {
        SetUI();
    }

    private void SetUI()
    {
        zombieKilledCountTxt.text = GameManager.Instance.GetTotalZombieKilled().ToString();

        float minute = GameManager.Instance.GetMinutePlayed();
        float second = GameManager.Instance.GetSecondPlayed();

        string secondTxt = second < 10 ? "0" + second.ToString() : second.ToString();
        string minTxt = minute < 10 ? "0" + minute : minute.ToString();

        gamePlayedTimeTxt.text = minTxt + ":" + secondTxt;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying())
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
