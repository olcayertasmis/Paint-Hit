using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using DG.Tweening;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [Header("Level")]
        [SerializeField] private TextMeshProUGUI levelText;
        private const string LevelString = "LEVEL : ";

        [Header("BG")]
        [SerializeField] private Image bg;
        [SerializeField] private List<Sprite> bgSprites = new List<Sprite>();

        [Header("Ball")]
        [SerializeField] private List<Image> balls;
        [SerializeField] private Image ballSprite;
        [SerializeField] private TextMeshProUGUI ballCountText;
        [SerializeField] private Transform ballsSpawner;

        [Header("Heart")]
        [SerializeField] private List<Image> hearts;
        [SerializeField] private Image heartSprite;
        [SerializeField] private Transform heartSpawner;

        private void Start()
        {
            balls = new List<Image>();
            hearts = new List<Image>();

            bg.sprite = bgSprites[Random.Range(0, bgSprites.Count)];

            levelText.text = LevelString + 1;
        }

        public void UpdateLevelText(int levelNumber)
        {
            levelText.text = LevelString + levelNumber;
        }

        public void FillBallSprites(int ballCount, int levelCount)
        {
            UpdateBallsCountText(ballCount, levelCount);

            for (int i = 0; i < levelCount; i++)
            {
                var newSprite = Instantiate(ballSprite, ballsSpawner);

                balls.Add(newSprite);
            }
        }

        public void DecreaseBallSprites(int ballCount, int levelCount)
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

        public void ResetBallSprites()
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

        public void FillHeartSprites(int heartCount)
        {
            for (int i = 0; i < heartCount; i++)
            {
                var newHeart = Instantiate(heartSprite, heartSpawner);

                hearts.Add(newHeart);
            }
        }

        public void DecreaseHeartSprites(int heartCount)
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
    }
}