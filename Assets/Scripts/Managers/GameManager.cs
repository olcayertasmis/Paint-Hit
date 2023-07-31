using System;
using Ball_Scripts;
using Handler_Scripts;
using UnityEngine;

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

        [Header("Action")]
        public Action GameOver;

        [Header("Audio")]
        [SerializeField] private AudioClip gameFail;

        private void Awake()
        {
            Time.timeScale = 0;
            ChangeState(GameStates.Menu);
        }

        public GameStates ChangeState(GameStates value)
        {
            _currentState = value;

            switch (value)
            {
                case GameStates.GameStart:
                    levelsHandler.MakeANewCircle();
                    Time.timeScale = 1;
                    ChangeState(GameStates.Playing);
                    break;
                case GameStates.Playing:
                    break;
                case GameStates.GameOver:
                    Singleton.Instance.AudioManager.PlaySound(gameFail);
                    GameOver?.Invoke();
                    break;
                case GameStates.Menu:
                    break;
                case GameStates.Restart:
                    Singleton.Instance.UIManager.StartGame();
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
        Menu,
        Restart
    }
}