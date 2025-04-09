using AppCoreModule.Scripts.UI.Screens;
using Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Screens
{
    public class SettingsScreen : BaseScreen
    {
        [Inject] private ScreenService _screenService;
        [Inject] private SettingsProvider _settingsProvider;
        
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Button _buttonBack;

        private void Start()
        {
            _buttonBack.onClick.AddListener(() => _screenService.GoBack());
            _toggle.onValueChanged.AddListener(value => _settingsProvider.SoundEnabled = value);
            _settingsProvider.SubscribeSoundEnabled(b => _toggle.SetIsOnWithoutNotify(b));
        }
    }
}