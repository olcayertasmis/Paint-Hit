using System.Collections;
using System.Runtime.CompilerServices;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Handler_Scripts
{
    public class BallHandler : MonoBehaviour
    {
        [Header("OtherComponents")]
        private GameManager _gameManager;
        private LevelsHandler _levelsHandler;
        private UIManager _uiManager;

        [Header("Ball")]
        private int _ballsCount;
        [SerializeField] private float ballSpeed;
        public static Color ballColor;
        [SerializeField] private Ball ballPrefab;
        [SerializeField] private Transform dummyBall;


        private void Start()
        {
            _gameManager = Singleton.Instance.GameManager;
            _uiManager = Singleton.Instance.UIManager;

            _levelsHandler = _gameManager.levelsHandler;

            GetRandomBallColor();

            _ballsCount = _levelsHandler.level;

            //_gameManager.CurrentState(GameStates.Gamestart);

            _uiManager.FillBallSprites(_ballsCount, _levelsHandler.level);
        }

        private void Update()
        {
            if (_ballsCount <= 0) return;

            if (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space))
            {
                HitBall();
            }
        }

        private void GetRandomBallColor()
        {
            ballColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }

        private void HitBall()
        {
            var newBall = Instantiate(ballPrefab, dummyBall.position, Quaternion.identity);
            newBall.ChangeColor(ballColor);
            newBall.Thrown(ballSpeed);

            _ballsCount--;
            _uiManager.DecreaseBallSprites(_ballsCount, _levelsHandler.level);

            if (_ballsCount > 0) return;
            StartCoroutine(InvokeFromLevelsHandler());
        }

        private IEnumerator InvokeFromLevelsHandler()
        {
            yield return new WaitForSeconds(.4f);
            _levelsHandler.MakeANewCircle();

            _uiManager.ResetBallSprites();

            yield return new WaitForSeconds(.6f);
            _ballsCount = _levelsHandler.level;

            GetRandomBallColor();

            _uiManager.FillBallSprites(_ballsCount, _levelsHandler.level);
        }
    }
}