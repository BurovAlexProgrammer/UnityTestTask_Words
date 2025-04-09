using GameCore.LevelGeneration;
using Services;
using UnityEngine;
using Zenject;

namespace Context
{
    public class GameContextInstaller : MonoInstaller
    {
        [SerializeField] private ScreenService _screenServicePrefab;
        
        public override void InstallBindings()
        {
            // LevelSerializer.GenerateFiles();
            Container.BindInterfacesAndSelfTo<RemoteConfigProvider>().AsSingle().NonLazy();
            Container.Bind<ScreenService>().FromComponentInNewPrefab(_screenServicePrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<LevelProvider>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<SessionResultService>().FromNew().AsCached();
        }
    }
}