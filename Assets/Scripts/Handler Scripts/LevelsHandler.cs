using System;
using System.Collections.Generic;
using Circle_Scripts;
using UnityEngine;
using DG.Tweening;

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

        private void Awake()
        {
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
        }

        private void SpawnCircle()
        {
            GameObject newCircle = Instantiate(circlePrefab, spawnedCircles);

            if (newCircle == null) return;
            newCircle.transform.position = new Vector3(0, 20, 23);
            newCircle.name = "Circle";

            _circles.Add(newCircle);
        }

        private void SlideDown(GameObject circle)
        {
            circle.transform.DOMoveY(circle.transform.position.y - 3f, 0.9f).SetEase(Ease.InBounce);
        }
    }
}