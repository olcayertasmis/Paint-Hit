using System;
using Managers;
using UnityEngine;

namespace Ball_Scripts
{
    public class Health : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private int heart;

        [Header("Actions")]
        public Action<int> OnDecreaseHeart;
        public Action<int> OnFillHeartSprites;

        private void Start()
        {
            OnFillHeartSprites?.Invoke(heart);
        }

        public void DecreaseHeart(int amount)
        {
            heart -= amount;

            if (heart == 0) Singleton.Instance.GameManager.ChangeState(GameStates.GameOver);

            OnDecreaseHeart?.Invoke(heart);
        }
    }
}