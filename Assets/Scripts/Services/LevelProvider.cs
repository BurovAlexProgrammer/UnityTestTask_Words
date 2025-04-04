using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameCore.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Services
{
    public class LevelProvider : IInitializable
    {
        private Dictionary<int, LevelData> _levels = new(4);

        private const int MaxLevel = 4;

        public void Initialize()
        {
            PreloadLevels().Forget();
        }

        public async UniTask<LevelData> GetLevel(int level)
        {
            if (level > MaxLevel)
                level = level % MaxLevel;
            
            //TODO wait level loading
            
            return _levels[level];
        }

        private async UniTask PreloadLevels()
        {
            for (var i = 1; i <= MaxLevel; i++)
            {
                var level1Json = await Addressables.LoadAssetAsync<TextAsset>($"level{i}");
                var level1 = JsonUtility.FromJson<LevelData>(level1Json.text);
                _levels.Add(i, level1);
            }
        }
    }
}