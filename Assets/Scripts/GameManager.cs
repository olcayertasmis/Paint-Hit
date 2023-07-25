using System;
using Handler_Scripts;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Instance")]
    public static GameManager Instance;

    [Header("GameState")]
    private GameStates _currentState;

    [Header("Handlers")]
    public LevelsHandler levelsHandler;
    public BallHandler ballHandler;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CurrentState(GameStates.Gamestart);
    }

    public GameStates CurrentState(GameStates value)
    {
        _currentState = value;

        switch (value)
        {
            case GameStates.Gamestart:
                levelsHandler.MakeANewCircle();
                CurrentState(GameStates.Playing);
                break;
            case GameStates.Playing:
                break;
            case GameStates.Gameover:
                break;
            case GameStates.Menu:
                break;
            default:
                break;
        }

        return _currentState;
    }
}

public enum GameStates
{
    Gamestart,
    Playing,
    Gameover,
    Menu
}