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

        private void Start()
        {
            ChangeState(GameStates.GameStart);
        }

        public GameStates ChangeState(GameStates value)
        {
            _currentState = value;

            switch (value)
            {
                case GameStates.GameStart:
                    levelsHandler.MakeANewCircle();
                    ChangeState(GameStates.Playing);
                    break;
                case GameStates.Playing:
                    Singleton.Instance.AudioManager.PlayMusicSound(AudioManager.SoundType.BackgroundMusic);
                    Singleton.Instance.UIManager.StartGame();
                    break;
                case GameStates.GameOver:
                    Singleton.Instance.AudioManager.PlayEffectSound(AudioManager.SoundType.Fail);
                    GameOver?.Invoke();
                    break;
                case GameStates.Menu:
                    Singleton.Instance.UIManager.Menu();
                    break;
                case GameStates.Restart:
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