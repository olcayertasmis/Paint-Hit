using System;
using System.Collections.Generic;
using Circle_Scripts;
using UnityEngine;
using DG.Tweening;
using Managers;
using Random = UnityEngine.Random;

namespace Handler_Scripts
{
    public class LevelsHandler : MonoBehaviour
    {
        [Header("Circle")]
        [SerializeField] private GameObject circlePrefab;
        [SerializeField] private Transform spawnedCircles;
        private List<GameObject> _circles;
        private bool _canChangeColor;

        [Header("Level")]
        public int level;

        private UIManager _uiManager;

        private void Awake()
        {
            _uiManager = Singleton.Instance.UIManager;

            _circles = new List<GameObject>();

            level = 0;
        }

        public void MakeANewCircle()
        {
            if (_circles != null)
            {
                foreach (var circle in _circles)
                {
                    if (circle == _circles[^1])
                    {
                        foreach (Transform _circle in circle.transform)
                        {
                            _circle.GetComponent<MeshRenderer>().material.DOColor(BallHandler.ballColor, 0.25f);
                        }
                    }

                    if (circle.transform.position.y <= -10)
                    {
                        circle.transform.DOScale(0, 1f).OnComplete(delegate { circle.gameObject.SetActive(false); });
                    }
                    else
                    {
                        SlideDown(circle);
                    }
                }
            }

            SpawnCircle();

            level++;
            _uiManager.UpdateLevelText(level);
        }

        private void SpawnCircle()
        {
            GameObject newCircle = Instantiate(circlePrefab, spawnedCircles);

            if (newCircle == null) return;
            newCircle.transform.position = new Vector3(0, 20, 23);
            newCircle.name = "Circle";

            SetObstacle(newCircle);

            _circles.Add(newCircle);
        }

        private void SlideDown(GameObject circle)
        {
            circle.transform.DOMoveY(circle.transform.position.y - 3f, 0.9f).SetEase(Ease.InBounce);
        }

        private void SetObstacle(GameObject circle)
        {
            int obstacleCount;
            if (level >= circle.transform.childCount * 2) obstacleCount = circle.transform.childCount - 1;
            else
            {
                if (level == 1) obstacleCount = 1;
                else
                {
                    obstacleCount = level / 2;
                }
            }

            for (int i = 1; i <= obstacleCount; i++)
            {
                int randomNumber = Random.Range(0, circle.transform.childCount);

                var circleTarget = circle.transform.GetChild(randomNumber);
                var obstacleMesh = circleTarget.gameObject.GetComponent<MeshRenderer>();

                if (!obstacleMesh.enabled)
                {
                    circleTarget.tag = "Red";
                    obstacleMesh.enabled = true;
                    obstacleMesh.material.DOColor(Color.black, 0.5f);
                }
                else i--;
            }
        }
    }
}