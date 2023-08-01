using System;
using System.Collections.Generic;
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
        [SerializeField] private AudioScripts.Sound[] musics;
        [SerializeField] private AudioScripts.Sound[] effects;

        [Header("AudioSources")]
        [SerializeField] private Transform musicFolder;
        [SerializeField] private Transform effectFolder;

        //private List<AudioSource> _musicSources;
        //private List<AudioSource> _effectSources;

        private void Awake()
        {
            foreach (var music in musics)
            {
                music.source = musicFolder.gameObject.AddComponent<AudioSource>();
                music.source.clip = music.clip;

                music.source.volume = music.volume;
                music.source.pitch = music.pitch;

                if (music.soundType == SoundType.BackgroundMusic) music.source.loop = true;
            }

            foreach (var effect in effects)
            {
                effect.source = effectFolder.gameObject.AddComponent<AudioSource>();
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

        public void ToggleMusics()
        {
            foreach (Transform folderType in gameObject.transform)
            {
                if (folderType != musicFolder) continue;
                var musicSources = folderType.GetComponents<AudioSource>();

                foreach (var musicSource in musicSources)
                {
                    musicSource.mute = !musicSource.mute;
                }
            }
        }

        public void ToggleEffects()
        {
            foreach (Transform folderType in gameObject.transform)
            {
                if (folderType != effectFolder) continue;
                var effectSources = folderType.GetComponents<AudioSource>();

                foreach (var effectSource in effectSources)
                {
                    effectSource.mute = !effectSource.mute;
                }
            }
        }
    }
}