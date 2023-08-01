using UnityEngine;
using DG.Tweening;
using Handler_Scripts;
using Managers;

namespace Circle_Scripts
{
    public class Circle : MonoBehaviour
    {
        [Header("RotateValue")]
        [SerializeField] private int minRotateValue, maxRotateValue;
        private int _rotationSpeed;

        [Header("OtherComponents")]
        private LevelsHandler _levelsHandler;

        private void Start()
        {
            _levelsHandler = Singleton.Instance.GameManager.GetLevelHandler();

            _rotationSpeed = Random.Range(minRotateValue, maxRotateValue);

            transform.DOMoveY(-2, .8f).SetEase(Ease.OutBounce);
        }

        private void Update()
        {
            RotateCircle();
        }

        private void RotateCircle()
        {
            transform.Rotate(Vector3.up * (Time.deltaTime * (_rotationSpeed + (_levelsHandler.level * (Random.Range(1, 3))))));
        }
    }
}