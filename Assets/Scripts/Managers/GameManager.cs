using System;
using Ball_Scripts;
using Handler_Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("GameState")]
        private GameStates _currentState;

        [Header("Handlers")]
        [SerializeField] private LevelsHandler levelsHandler;
        [SerializeField] private BallHandler ballHandler;

        [Header("Other Scripts")]
        [SerializeField] private Health health;

        private void Start()
        {
            CurrentState(GameStates.GameStart);
        }

        public GameStates CurrentState(GameStates value)
        {
            _currentState = value;

            switch (value)
            {
                case GameStates.GameStart:
                    levelsHandler.MakeANewCircle();
                    CurrentState(GameStates.Playing);
                    break;
                case GameStates.Playing:
                    break;
                case GameStates.GameOver:
                    break;
                case GameStates.Menu:
                    break;
                default:
                    break;
            }

            return _currentState;
        }

        #region Helpers

        public LevelsHandler GetLevelHandler() => levelsHandler;
        public BallHandler GetBallHandler() => ballHandler;
        public Health GetHealthHandler() => health;

        #endregion
    }

    public enum GameStates
    {
        GameStart,
        Playing,
        GameOver,
        Menu
    }
}