using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource, effectSource;

        public void PlaySound(AudioClip clip)
        {
            effectSource.PlayOneShot(clip);
        }

        public void ToggleEffects()
        {
            effectSource.mute = !effectSource.mute;
        }

        public void ToggleMusic()
        {
            musicSource.mute = !musicSource.mute;
        }
    }
}