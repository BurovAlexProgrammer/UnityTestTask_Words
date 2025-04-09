using System;
using AppCoreModule.Scripts.Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Services
{
    public class AudioService : MonoBehaviour
    {
        [Inject] private SettingsProvider _settingsProvider;
        
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        [SerializeField] private AudioEvent _wooshAudioEvent;
        [SerializeField] private AudioEvent _boolkAudioEvent;
        [SerializeField] private AudioSource _audioSource;

        private void Start()
        {
            _settingsProvider.SubscribeSoundEnabled(SetAudioEnabled);
        }

        public void SetAudioEnabled(bool isEnabled)
        {
            _audioMixerGroup.audioMixer.SetFloat("Volume", isEnabled ? 0f : -80f);
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