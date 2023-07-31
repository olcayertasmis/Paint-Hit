using System;
using Managers;
using UnityEngine;

namespace Ball_Scripts
{
    public class Health : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private int heart;

        [Header("Managers")]
        private UIManager _uiManager;

        [Header("Actions")]
        public Action<int> OnDecreaseHeart;
        public Action<int> OnFillHeartSprites;

        private void Start()
        {
            _uiManager = Singleton.Instance.UIManager;

            OnFillHeartSprites?.Invoke(heart);
            //_uiManager.FillHeartSprites(heart);
        }

        public void DecreaseHeart(int amount)
        {
            heart -= amount;

            OnDecreaseHeart?.Invoke(heart);
            //_uiManager.DecreaseHeartSprites(heart);
        }
    }
}