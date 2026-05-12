using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private enum State
    {
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    [Header("References")]
    [SerializeField]
    private Survivalist survivalistGroup;
    private State state;

    [Header("Settings")]
    private float countDownToStartTimerMax = 3f;
    private float countDownToStartTimer = 3f;
    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        state = State.CountDownToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void Update()
    {
        Debug.Log(state);
        switch(state)
        {
            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;

                if (countDownToStartTimer <= 0f)
                {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GamePlaying:
                if (survivalistGroup.AllSurvivalistsAreDead())
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        if (state != State.GamePlaying) return;
        ToggleGamePause();
    }

    public void ToggleGamePause()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            StopGameTime();
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            RunGameTime();
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
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
        return state == State.GamePlaying;
    }

    public bool isCountingToStart()
    {
        return state == State.CountDownToStart;
    }

    public float GetCountDownToStartTimer()
    {
        return countDownToStartTimer;
    }
}
