using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }
    public static GameManager Instance { get; private set; }

    public GameState State { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChangeGameState(GameState.Playing);
    }

    private void Update()
    {
        if (InputManager.Instance.IsPressEscape())
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (State == GameState.Playing)
        {
            ChangeGameState(GameState.Paused);
        }
        else if (State == GameState.Paused)
        {
            ChangeGameState(GameState.Playing);
        }
    }

    public void ChangeGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.Playing:
                HandlePlaying();
                break;
            case GameState.Paused:
                HandlePaused();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
        }
    }

    private void HandlePlaying()
    {
        Debug.Log("Game is running");
        Time.timeScale = 1f;
    }

    private void HandlePaused()
    {
        Debug.Log("Game paused");
        Time.timeScale = 0f;
    }

    private void HandleGameOver()
    {
        Debug.Log("Game over");
        Time.timeScale = 0f;
    }
}
