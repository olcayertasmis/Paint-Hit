using System;
using System.Collections;
using Ball_Scripts;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Handler_Scripts
{
    public class BallHandler : MonoBehaviour
    {
        [Header("OtherComponents")]
        private LevelsHandler _levelsHandler;
        private ObjectPool _objectPool;

        [Header("Ball")]
        private int _ballsCount;
        [SerializeField] private float ballSpeed;
        [SerializeField] private Transform dummyBall;
        [HideInInspector] public int accurateBall;
        [HideInInspector] public Color ballColor;
        private bool _isShoot;

        [Header("Actions")]
        public Action<int, int> OnHitBall;
        public Action<int, int> OnFillBallSprites;

        private void Start()
        {
            _isShoot = true;

            _objectPool = Singleton.Instance.ObjectPool;

            _levelsHandler = Singleton.Instance.GameManager.GetLevelHandler();

            GetRandomBallColor();

            _ballsCount = _levelsHandler.level + 2;

            OnFillBallSprites?.Invoke(_ballsCount, _levelsHandler.level + 2);
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                if (_ballsCount <= 0 && !_isShoot) return;
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
            _isShoot = false;

            yield return new WaitForSeconds(.4f);
            accurateBall = 0;

            _levelsHandler.MakeANewCircle();

            yield return new WaitForSeconds(.8f);
            _ballsCount = _levelsHandler.level + 2;

            GetRandomBallColor();

            OnFillBallSprites?.Invoke(_ballsCount, _levelsHandler.level + 2);

            yield return new WaitForSeconds(2f);
            _isShoot = true;
        }
    }
}