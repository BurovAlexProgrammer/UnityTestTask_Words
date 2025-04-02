using Services;
using Zenject;

namespace Context
{
    public class ProjectContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SettingsProvider>().FromNew().AsSingle();
        }
    }
}