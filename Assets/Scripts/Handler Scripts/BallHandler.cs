using System;
using System.Collections;
using Ball_Scripts;
using DG.Tweening;
using Managers;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Handler_Scripts
{
    public class BallHandler : MonoBehaviour
    {
        [Header("OtherComponents")]
        private GameManager _gameManager;
        private LevelsHandler _levelsHandler;
        private UIManager _uiManager;
        private ObjectPool _objectPool;

        [Header("Ball")]
        private int _ballsCount;
        [SerializeField] private float ballSpeed;
        [SerializeField] private Ball ballPrefab;
        [SerializeField] private Transform dummyBall;
        [HideInInspector] public int accurateBall;
        [HideInInspector] public Color ballColor;

        [Header("Actions")]
        public Action<int, int> OnHitBall;
        public Action<int, int> OnFillBallSprites;

        private void Start()
        {
            _gameManager = Singleton.Instance.GameManager;
            _uiManager = Singleton.Instance.UIManager;
            _objectPool = Singleton.Instance.ObjectPool;

            _levelsHandler = _gameManager.GetLevelHandler();

            GetRandomBallColor();

            _ballsCount = _levelsHandler.level + 2;

            OnFillBallSprites?.Invoke(_ballsCount, _levelsHandler.level + 2);
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                if (_ballsCount <= 0) return;
                HitBall();
            }
        }

        private void GetRandomBallColor()
        {
            ballColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }

        private void HitBall()
        {
            var newBall = _objectPool.GetPooledObject(0).GetComponent<Ball>();
            if (!newBall) return;

            newBall.transform.position = dummyBall.position;
            newBall.GetComponent<Collider>().enabled = true;

            newBall.ChangeColor(ballColor);
            newBall.Thrown(ballSpeed);

            _ballsCount--;

            OnHitBall?.Invoke(_ballsCount, _levelsHandler.level + 2);
        }

        public IEnumerator InvokeFromLevelsHandler()
        {
            yield return new WaitForSeconds(.4f);
            accurateBall = 0;

            _levelsHandler.MakeANewCircle();

            yield return new WaitForSeconds(.6f);
            _ballsCount = _levelsHandler.level + 2;

            GetRandomBallColor();

            OnFillBallSprites?.Invoke(_ballsCount, _levelsHandler.level + 2);
        }
    }
}