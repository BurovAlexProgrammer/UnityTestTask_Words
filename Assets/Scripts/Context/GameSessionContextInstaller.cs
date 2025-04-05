using Services;
using UnityEngine;
using Zenject;

namespace Context
{
    public class GameSessionContextInstaller : MonoInstaller
    {
        [SerializeField] private GameSessionService _gameSessionService;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameSessionService>().FromInstance(_gameSessionService).AsSingle().NonLazy();
        }
    }
}