using System;
using Managers;
using UnityEngine;

namespace Ball_Scripts
{
    public class Health : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private int heart;

        [Header("Other Scripts")]
        private UIManager _uiManager;

        private void Awake()
        {
            _uiManager = Singleton.Instance.UIManager;
        }

        private void Start()
        {
            _uiManager.FillHeartSprites(heart);
        }

        public void DecreaseHeart(int amount)
        {
            heart -= amount;

            _uiManager.DecreaseHeartSprites(heart);
        }
    }
}