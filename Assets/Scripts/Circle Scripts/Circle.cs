using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Handler_Scripts;
using Managers;

namespace Circle_Scripts
{
    public class Circle : MonoBehaviour
    {
        [Header("RotateValue")]
        [SerializeField] private int minRotateValue;
        [SerializeField] private int maxRotateValue;
        private int _rotationSpeed;

        [Header("OtherComponents")]
        private LevelsHandler _levelsHandler;
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = Singleton.Instance.GameManager;
            _levelsHandler = _gameManager.levelsHandler;

            _rotationSpeed = Random.Range(minRotateValue, maxRotateValue);

            transform.DOMoveY(0, 1f).SetEase(Ease.OutBounce).OnComplete(() => { Test(); });
        }

        private void Update()
        {
            RotateCircle();
        }

        private void Test()
        {
            Debug.Log("Test");
        }

        private void RotateCircle()
        {
            transform.Rotate(Vector3.up * (Time.deltaTime * (_rotationSpeed + (_levelsHandler.level * (Random.Range(1, 3))))));
        }
    }
}