using AppCoreModule.Scripts.UI.Screens;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Services
{
    public class ResourceProvider
    {
        public enum ScreenNames
        {
            MainMenuScreen,
            SettingsScreen,
            GameScreen,
            ResultScreen
        }
        
        public static async UniTask<BaseScreen> GetScreenPrefabAsync(ScreenNames screenName)
        {
            var prefab = await Addressables.LoadAssetAsync<GameObject>(screenName.ToString());
            var baseScreen = prefab.GetComponent<BaseScreen>();
            return baseScreen;
        }
    }
}