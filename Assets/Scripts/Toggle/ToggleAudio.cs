using Managers;
using UnityEngine;

namespace Toggle
{
    public class ToggleAudio : MonoBehaviour
    {
        [SerializeField] private bool toggleMusic, toggleEffects;

        public void Toggle()
        {
            if (toggleEffects) Singleton.Instance.AudioManager.ToggleEffects();
            if (toggleMusic) Singleton.Instance.AudioManager.ToggleMusic();
        }
    }
}