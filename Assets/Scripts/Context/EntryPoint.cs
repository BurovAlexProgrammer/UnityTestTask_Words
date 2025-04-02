using Cysharp.Threading.Tasks;
using Services;
using UnityEngine;
using Zenject;
using ResourceProvider = Services.ResourceProvider;

namespace Context
{
    public class EntryPoint : MonoBehaviour
    {
        [Inject] private ScreenService _screenService;
        [Inject] private SettingsProvider _settingsProvider;

        private void Start()
        {
            InitAsync().Forget();
        }

        private async UniTask InitAsync()
        {
            _screenService.Init(true, _settingsProvider.TransitEffectSettings);
            var screen = await ResourceProvider.GetScreenPrefabAsync(ResourceProvider.ScreenNames.MainMenuScreen);
            _screenService.OpenScreen(screen);
        }
    }
}