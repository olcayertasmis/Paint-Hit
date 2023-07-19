using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace Handler_Scripts
{
    public class BallHandler : MonoBehaviour
    {
        [Header("OtherComponents")]
        private GameManager _gameManager;
        private LevelsHandler _levelsHandler;

        [Header("Ball")]
        private int _ballsCount;
        [SerializeField] private float ballSpeed;
        public static Color ballColor;
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private Transform dummyBall;


        private void Start()
        {
            _gameManager = GameManager.Instance;
            _levelsHandler = _gameManager.levelsHandler;

            SetBallColorRandom();

            _ballsCount = _levelsHandler.level;
        }

        private void Update()
        {
            if (_ballsCount <= 0) return;

            if (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space))
            {
                HitBall();
            }
        }

        private void SetBallColorRandom()
        {
            ballColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }

        private void HitBall()
        {
            GameObject thrownBall = Instantiate(ballPrefab, dummyBall.position, Quaternion.identity);
            thrownBall.GetComponent<MeshRenderer>().material.color = ballColor;
            thrownBall.GetComponent<Rigidbody>().AddForce(Vector3.forward * ballSpeed, ForceMode.Impulse);

            _ballsCount--;

            if (_ballsCount > 0) return;
            StartCoroutine(InvokeFromLevelsHandler());
        }

        private IEnumerator InvokeFromLevelsHandler()
        {
            yield return new WaitForSeconds(.4f);
            _levelsHandler.MakeANewCircle();


            yield return new WaitForSeconds(.6f);
            _ballsCount = _levelsHandler.level;
            SetBallColorRandom();
        }
    }
}