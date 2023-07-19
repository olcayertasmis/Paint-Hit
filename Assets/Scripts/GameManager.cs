using System;
using Handler_Scripts;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("GameManager")]
    public static GameManager Instance;

    [Header("GameState")]
    private GameStates _currentstate;

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
        _currentstate = value;

        switch (value)
        {
            case GameStates.Gamestart:
                levelsHandler.MakeANewCircle();
                CurrentState(GameStates.Playıng);
                break;
            case GameStates.Playıng:
                break;
            case GameStates.Gameover:
                break;
            case GameStates.Menu:
                break;
            default:
                break;
        }

        return _currentstate;
    }
}

public enum GameStates
{
    Gamestart,
    Playıng,
    Gameover,
    Menu
}