using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    [Header("References")]
    [SerializeField]
    private Survivalist survivalistGroup;
    private State state;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    public void StopGameTime()
    {
        Time.timeScale = 0f;    
    }

    public void RunGameTime()
    {
        Time.timeScale = 1f;
    }

    public void GameOver()
    {

    }
}
