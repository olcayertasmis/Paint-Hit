using System;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public enum SoundType
        {
            BallHit,
            CircleComplete,
            Fail,
            BackgroundMusic
        }

        [Header("Sounds")]
        [SerializeField] private Sound.Sound[] musics;
        [SerializeField] private Sound.Sound[] effects;

        private void Awake()
        {
            foreach (var music in musics)
            {
                music.source = gameObject.AddComponent<AudioSource>();
                music.source.clip = music.clip;

                music.source.volume = music.volume;
                music.source.pitch = music.pitch;

                if (music.soundType == SoundType.BackgroundMusic) music.source.loop = true;
            }

            foreach (var effect in effects)
            {
                effect.source = gameObject.AddComponent<AudioSource>();
                effect.source.clip = effect.clip;

                effect.source.volume = effect.volume;
                effect.source.pitch = effect.pitch;
            }
        }

        public void PlayEffectSound(SoundType soundType)
        {
            var effectSound = Array.Find(effects, effect => effect.soundType == soundType);
            effectSound.source.Play();
        }

        public void PlayMusicSound(SoundType soundType)
        {
            var musicSound = Array.Find(musics, music => music.soundType == soundType);
            musicSound.source.Play();
        }

        public void ToggleMusic()
        {
            foreach (var music in musics)
            {
                music.source = gameObject.GetComponent<AudioSource>();

                music.source.mute = !music.source.mute;
            }
        }

        public void ToggleEffects()
        {
            foreach (var effect in effects)
            {
                effect.source = gameObject.GetComponent<AudioSource>();

                effect.source.mute = !effect.source.mute;
            }
        }
    }
}