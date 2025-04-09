using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameCore.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Services
{
    public class LevelProvider : IInitializable
    {
        [Inject] private RemoteConfigProvider _remoteConfigProvider;
        
        private Dictionary<int, LevelData> _levels = new(4);
        private bool _initialised;
        private bool _preloaded;

        public const int MaxLevel = 4;
        
        public void Initialize()
        {
            InitAsync().Forget();
            PreloadLevels().Forget();
        }
        
        public async UniTask<LevelData> GetLevel(int level)
        {
            while (!_initialised)
                await UniTask.Yield();
            
            if (level == 1) 
                return _levels[level];

            while (!_preloaded)
                await UniTask.Yield();
            
            return _levels[level];
        }

        private async UniTask InitAsync()
        {
            var level1Json = await Addressables.LoadAssetAsync<TextAsset>("level1");
            var level1 = JsonUtility.FromJson<LevelData>(level1Json.text);
            _levels.Add(1, level1);
            _initialised = true;
        }

        private async UniTask PreloadLevels()
        {
            var levelConfigs = await _remoteConfigProvider.GetLevelConfigs();
            
            for (var i = 2; i <= MaxLevel; i++)
            {
                var level = levelConfigs.Levels.FirstOrDefault(x => x.Level == i);

                if (level.Equals(default))
                {
                    Debug.LogError($"Level{i} not found on remote config");
                    continue;
                }
                
                _levels.Add(level.Level, level);
            }

            _preloaded = true;
        }
    }
}