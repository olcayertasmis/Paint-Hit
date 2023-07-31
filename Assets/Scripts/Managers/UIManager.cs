using System;
using System.Collections.Generic;
using Ball_Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using DG.Tweening;
using Handler_Scripts;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [Header("Level")]
        [SerializeField] private TextMeshProUGUI levelText;
        private const string LevelString = "LEVEL : ";

        [Header("BackGround")]
        [SerializeField] private Image bg;
        [SerializeField] private List<Sprite> bgSprites;

        [Header("Ball")]
        [SerializeField] private List<Image> balls;
        [SerializeField] private Image ballSprite;
        [SerializeField] private TextMeshProUGUI ballCountText;
        [SerializeField] private Transform ballsSpawner;

        [Header("Heart")]
        [SerializeField] private List<Image> hearts;
        [SerializeField] private Image heartSprite;
        [SerializeField] private Transform heartSpawner;

        [Header("Managers")]
        private GameManager _gameManager;
        private BallHandler _ballHandler;

        [Header("Other Scripts")]
        private Health _health;

        [Header("Screen Panels")]
        [SerializeField] private GameObject failScreen;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject gamePlayScreen;
        [SerializeField] private GameObject menuScreen;


        private void Awake()
        {
            _gameManager = Singleton.Instance.GameManager;
            _ballHandler = _gameManager.GetBallHandler();
            _health = _gameManager.GetHealthHandler();
        }

        private void Start()
        {
            bg.sprite = bgSprites[Random.Range(0, bgSprites.Count)];

            levelText.text = LevelString + 1;
        }

        private void OnEnable()
        {
            _ballHandler.OnHitBall += BallHandler_OnHitBall;
            _gameManager.GetLevelHandler().OnLevelUp += LevelHandler_OnLevelUp;
            _ballHandler.OnFillBallSprites += BallHandler_OnFillBallSprites;
            _health.OnDecreaseHeart += Health_DecreaseHeart;
            _health.OnFillHeartSprites += Health_FillHeartSprites;
            _gameManager.GameOver += OnGameOver;
        }

        #region EventListeners

        private void BallHandler_OnHitBall(int ballCount, int levelCount)
        {
            DecreaseBallSprites(ballCount, levelCount);
        }

        private void LevelHandler_OnLevelUp(int level)
        {
            UpdateLevelText(level);
        }

        private void BallHandler_OnFillBallSprites(int ballCount, int levelCount)
        {
            FillBallSprites(ballCount, levelCount);
        }

        private void Health_DecreaseHeart(int heartCount)
        {
            DecreaseHeartSprites(heartCount);
        }

        private void Health_FillHeartSprites(int heartCount)
        {
            FillHeartSprites(heartCount);
        }

        private void OnGameOver()
        {
            GameOver();
        }

        #endregion

        private void UpdateLevelText(int levelNumber)
        {
            levelText.text = LevelString + levelNumber;
        }

        private void FillBallSprites(int ballCount, int levelCount)
        {
            ResetBallSprites();

            UpdateBallsCountText(ballCount, levelCount);

            for (int i = 0; i < levelCount; i++)
            {
                var newSprite = Instantiate(ballSprite, ballsSpawner);

                balls.Add(newSprite);
            }
        }

        private void DecreaseBallSprites(int ballCount, int levelCount)
        {
            bool isRemove = false;

            balls.RemoveAt(balls.Count - 1);

            foreach (Transform ball in ballsSpawner)
            {
                if (!ball.gameObject.activeSelf) continue;
                if (isRemove) continue;
                ball.gameObject.SetActive(false);
                isRemove = true;
            }

            UpdateBallsCountText(ballCount, levelCount);
        }

        private void ResetBallSprites()
        {
            balls.Clear();

            foreach (Transform ball in ballsSpawner)
            {
                ball.gameObject.SetActive(false);
            }
        }

        private void UpdateBallsCountText(int ballCount, int levelCount)
        {
            ballCountText.text = ballCount + " / " + levelCount;
        }

        private void FillHeartSprites(int heartCount)
        {
            for (int i = 0; i < heartCount; i++)
            {
                var newHeart = Instantiate(heartSprite, heartSpawner);

                hearts.Add(newHeart);
            }
        }

        private void DecreaseHeartSprites(int heartCount)
        {
            switch (heartCount)
            {
                case 2:
                    hearts[heartCount].DOColor(Color.black, 0.5f);
                    break;
                case 1:
                    hearts[heartCount].DOColor(Color.black, 0.5f);
                    break;
                case 0:
                    hearts[heartCount].DOColor(Color.black, 0.5f);
                    break;
            }
        }

        public void PauseGameButton()
        {
            Time.timeScale = 0;
            ChangePanelsStatus(false, false, true, false);
        }

        public void ContinueGameButton()
        {
            Time.timeScale = 1;
            ChangePanelsStatus(true, false, false, false);
        }

        public void HomeButton()
        {
            ChangePanelsStatus(false, false, false, true);
            _gameManager.ChangeState(GameStates.Menu);
        }

        public void StartGame()
        {
            ChangePanelsStatus(true, false, false, false);
            _gameManager.ChangeState(GameStates.GameStart);
        }

        private void GameOver()
        {
            ChangePanelsStatus(false, true, false, false);
        }

        public void RestartGame()
        {
            _gameManager.ChangeState(GameStates.Restart);
            var activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.name);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        private void ChangePanelsStatus(bool gamePlayScreenBool, bool failScreenBool, bool pauseScreenBool, bool menuScreenBool)
        {
            gamePlayScreen.SetActive(gamePlayScreenBool);
            failScreen.SetActive(failScreenBool);
            pauseScreen.SetActive(pauseScreenBool);
            menuScreen.SetActive(menuScreenBool);
        }

        private void OnDisable()
        {
            _ballHandler.OnHitBall -= BallHandler_OnHitBall;
            _gameManager.GetLevelHandler().OnLevelUp -= LevelHandler_OnLevelUp;
            _ballHandler.OnFillBallSprites -= BallHandler_OnFillBallSprites;
            _health.OnDecreaseHeart -= Health_DecreaseHeart;
            _health.OnFillHeartSprites -= Health_FillHeartSprites;
        }
    }
}