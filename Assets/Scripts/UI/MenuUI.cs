using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Button playBtn;
    [SerializeField]
    private Button quitBtn;

    private void Awake()
    {
        playBtn.onClick.AddListener(() => 
        {
            Loader.Load(Loader.Scene.GameScene);
        });

        quitBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
