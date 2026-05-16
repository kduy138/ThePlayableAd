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
        string second = "00";

        if (int.Parse(second) >= 60) second = "00";

        zombieKilledCountTxt.text = GameManager.Instance.GetTotalZombieKilled().ToString();

        float totalSecondPlayed =  Mathf.Ceil(GameManager.Instance.GetGamePlayedTime());
        float minute = totalSecondPlayed < 60 ? 0 : Mathf.Ceil(totalSecondPlayed / 60);
        second = totalSecondPlayed - (minute * 2) < 10 ? "0" + (totalSecondPlayed - (minute * 2)).ToString() : (totalSecondPlayed - (minute * 2)).ToString();
        string minTxt = minute < 10 ? "0" + minute : minute.ToString();

        gamePlayedTimeTxt.text = minTxt + ":" + second;
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
