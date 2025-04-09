using AppCoreModule.Scripts.Audio;
using UnityEngine;
using UnityEngine.Audio;

namespace Services
{
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        [SerializeField] private AudioEvent _wooshAudioEvent;
        [SerializeField] private AudioEvent _boolkAudioEvent;
        [SerializeField] private AudioSource _audioSource;

        private const string MixerName = "";
        
        public void SetAudioEnabled(bool enabled)
        {
            _audioMixerGroup.audioMixer.SetFloat(MixerName, enabled ? 0f : -80f);
        }

        //Просто нету времени писать нормально
        public void PlayWoosh()
        {
            _wooshAudioEvent.Play(_audioSource);
        }

        public void PlayBoolk()
        {
            _boolkAudioEvent.Play(_audioSource);
        }
    }
}