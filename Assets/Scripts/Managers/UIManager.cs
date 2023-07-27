using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private List<Sprite> balls = new List<Sprite>();
        [SerializeField] private Sprite ballSprite;
        [SerializeField] private TextMeshProUGUI ballCountText;
        [SerializeField] private Transform ballsSpawner;

        private void Awake()
        {
            bg.sprite = bgSprites[Random.Range(0, bgSprites.Count)];
        }

        void Start()
        {
            levelText.text = _levelString + 1;
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
        }

        private void UpdateBallsCountText(int ballCount, int levelCount)
        {
            ballCountText.text = ballCount + " / " + levelCount;
        }
    }
}