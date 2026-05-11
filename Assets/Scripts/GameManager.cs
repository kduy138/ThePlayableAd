using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;

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

    [Header("Settings")]
    private float waitingToStartTimer = 1f;
    private float countDownToStartTimer = 3f;
    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
        RunGameTime();
    }

    private void Update()
    {
        switch(state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;

                if (waitingToStartTimer <= 0f)
                {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountDownToStart:

                break;

            case State.GamePlaying:
                if (survivalistGroup.AllSurvivalistsAreDead())
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                StopGameTime();
                break;
        }
    }

    public void StopGameTime()
    {
        Time.timeScale = 0f;    
    }

    public void RunGameTime()
    {
        Time.timeScale = 1f;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public bool IsGamePlaying()
    {
        return state == State.WaitingToStart;
    }

    public bool isCountingToStart()
    {
        return state == State.CountDownToStart;
    }
}
