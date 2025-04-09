using Services;
using UnityEngine;
using Zenject;

namespace Context
{
    public class ProjectContextInstaller : MonoInstaller
    {
        [SerializeField] private AudioService _audioService;
        
        public override void InstallBindings()
        {
            Container.Bind<SettingsProvider>().FromNew().AsSingle();
            Container.Bind<AudioService>().FromInstance(_audioService).AsSingle();
        }
    }
}