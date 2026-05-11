using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Survivalist survivalistGroup;
    [SerializeField]
    private Button tryAgainBtn;
    [SerializeField]
    private Button backToMenuBtn;

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
