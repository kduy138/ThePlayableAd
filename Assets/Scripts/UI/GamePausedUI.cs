using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Button resumeBtn;
    [SerializeField]
    private Button backToMenuBtn;

    private void Awake()
    {
        resumeBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.ToggleGamePause();
        });

        backToMenuBtn.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MenuScene);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
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
