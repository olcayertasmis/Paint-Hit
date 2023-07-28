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
        private string _levelString = "LEVEL : ";

        [Header("BG")]
        [SerializeField] private Image bg;
        [SerializeField] private List<Sprite> bgSprites = new List<Sprite>();

        [Header("Ball")]
        [SerializeField] private List<Image> balls = new List<Image>();
        [SerializeField] private Image ballSprite;
        [SerializeField] private TextMeshProUGUI ballCountText;
        [SerializeField] private Transform ballsSpawner;

        [Header("Heart")]
        [SerializeField] private List<Image> hearts = new List<Image>();
        [SerializeField] private Image heartSprite;
        [SerializeField] private Transform heartSpawner;
        private int heartCountt = 3;

        private void Awake()
        {
            bg.sprite = bgSprites[Random.Range(0, bgSprites.Count)];
        }

        private void Start()
        {
            levelText.text = _levelString + 1;

            FillHeartSprites();
        }

        public void UpdateLevelText(int levelNumber)
        {
            levelText.text = _levelString + levelNumber;
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
            balls.RemoveAt(balls.Count - 1);

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

        private void FillHeartSprites()
        {
            for (int i = 0; i < heartCountt; i++)
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