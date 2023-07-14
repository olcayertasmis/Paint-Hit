using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Handler_Scripts;

namespace Circle_Scripts
{
    public class Circle : MonoBehaviour
    {
        private void Start()
        {
            transform.DOMoveY(0, 0.75f).SetEase(Ease.InCirc).OnComplete(() => { Test(); });
        }

        private void Update()
        {
            RotateCircle();
        }

        private void Test()
        {
            Debug.Log("sd");
        }

        private void RotateCircle()
        {
            transform.Rotate(Vector3.up*Time.deltaTime*BallHandler.RotationSpeed);
        }
    }
}