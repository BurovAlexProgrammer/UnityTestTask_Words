using AppCoreModule.Scripts.UI.TransitEffects.Settings;
using UI.TransitEffects;
using UnityEngine;

namespace Services
{
    public class SettingsProvider
    {
        public TransitEffectSettings TransitEffectSettings = new TransitEffectSettings()
        {
            CloseScreenEffect = new CloseScreenEffect(),
            OpenScreenEffect = new OpenScreenEffect()
        };
    }
}