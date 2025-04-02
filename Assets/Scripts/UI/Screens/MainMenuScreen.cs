using AppCoreModule.Scripts.Services;
using AppCoreModule.Scripts.UI.Screens;
using Cysharp.Threading.Tasks;
using Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using ResourceProvider = Services.ResourceProvider;
using ScreenService = Services.ScreenService;

namespace UI.Screens
{
    public class MainMenuScreen : BaseScreen
    {
        [Inject] private ScreenService _screenService;
        
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _playButton;

        private void Awake()
        {
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            _playButton.onClick.AddListener(OnPlayButtonClicked);
        }
        
        private void OnDestroy()
        {
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            PlayGame().Forget();
        }

        private void OnSettingsButtonClicked()
        {
            GoToSettings().Forget();
        }

        private async UniTask GoToSettings()
        {
            var settingsScreenPrefab = await ResourceProvider.GetScreenPrefabAsync(ResourceProvider.ScreenNames.SettingsScreen);
            _screenService.OpenScreen(settingsScreenPrefab);
        }

        private async UniTask PlayGame()
        {
            var gameScreenPrefab = await ResourceProvider.GetScreenPrefabAsync(ResourceProvider.ScreenNames.GameScreen);
            _screenService.OpenScreen(gameScreenPrefab);
        }
    }
}