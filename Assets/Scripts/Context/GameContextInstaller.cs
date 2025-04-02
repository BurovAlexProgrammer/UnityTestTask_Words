using AppCoreModule.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Context
{
    public class GameContextInstaller : MonoInstaller
    {
        [SerializeField] private ScreenService _screenServicePrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<ScreenService>().FromComponentInNewPrefab(_screenServicePrefab).AsSingle();
        }
    }
}