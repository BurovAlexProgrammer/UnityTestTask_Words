using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameCore.Models;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using UnityEngine;
using Zenject;
using static Unity.Services.RemoteConfig.RemoteConfigService;

namespace Services
{
    public class RemoteConfigProvider : IInitializable
    {
        private bool _initialised;
        private LevelConfigs _levelsConfigs;

        public void Initialize()
        {
            InitAsync().Forget();
        }

        public async UniTask<LevelConfigs> GetLevelConfigs()
        {
            while (!_initialised)
                await UniTask.Yield();
            
            return _levelsConfigs;
        }

        private async UniTask InitAsync()
        {
            if (Utilities.CheckForInternetConnection())
            {
                await UnityServices.InitializeAsync();

                if (!AuthenticationService.Instance.IsSignedIn)
                {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                }
            }

            Instance.FetchCompleted += ApplyRemoteSettings;
            Instance.FetchConfigs(new userAttributes(), new appAttributes());
        }

        private void ApplyRemoteSettings(ConfigResponse obj)
        {
            _initialised = true;
            Debug.Log("RemoteConfigService: appConfig fetched: " + Instance.appConfig.config.ToString());
            
            var data = Instance.appConfig.config.ToObject<appAttributes>();
            _levelsConfigs = JsonUtility.FromJson<LevelConfigs>(data.LevelsJson);
        }

        private struct userAttributes
        {
        }

        private struct appAttributes
        {
            public string LevelsJson;
        }
    }
}
