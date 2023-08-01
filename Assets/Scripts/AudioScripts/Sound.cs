using Managers;
using UnityEngine;

namespace AudioScripts
{
    [System.Serializable]
    public class Sound
    {
        [Header("SoundType")]
        public AudioManager.SoundType soundType;

        [Header("SoundClip")]
        public AudioClip clip;

        [Header("SoundSettings")]
        [Range(0, 1f)] public float volume;
        [Range(0, 1f)] public float pitch;

        [Header("AudioSource")]
        [HideInInspector] public AudioSource source;
    }
}