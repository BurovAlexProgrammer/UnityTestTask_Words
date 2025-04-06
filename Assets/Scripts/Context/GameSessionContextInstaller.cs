using GameCore.LevelControl;
using Services;
using UnityEngine;
using Zenject;

namespace Context
{
    public class GameSessionContextInstaller : MonoInstaller
    {
        [SerializeField] private GameSessionController _gameSessionController;
        [SerializeField] private LevelController _levelController;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameSessionController>().FromInstance(_gameSessionController).AsSingle();
            Container.BindInterfacesAndSelfTo<LevelController>().FromInstance(_levelController).AsSingle();
        }
    }
}