using System;
using AppCoreModule.Scripts.UI.TransitEffects.Settings;
using UI.TransitEffects;
using Unity.VisualScripting;
using UnityEngine;

namespace Services
{
    public class SettingsProvider
    {
        private event Action<bool> SoundEnableChanged;

        public void SubscribeSoundEnabled(Action<bool> action)
        {
            action(SoundEnabled);
            SoundEnableChanged += action;
        }
        
        public bool SoundEnabled
        {
            get => PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
            set
            {
                PlayerPrefs.SetInt("SoundEnabled", value ? 1 : 0);
                SoundEnableChanged?.Invoke(value);
            }
        }

        public TransitEffectSettings TransitEffectSettings = new TransitEffectSettings()
        {
            CloseScreenEffect = new CloseScreenEffect(),
            OpenScreenEffect = new OpenScreenEffect()
        };
    }
}