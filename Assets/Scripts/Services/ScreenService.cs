using AppCoreModule.Scripts.UI.Screens;
using UnityEngine;
using Zenject;

namespace Services
{
    public class ScreenService : AppCoreModule.Scripts.Services.ScreenService
    {
        [Inject] private DiContainer _diContainer;

        public Canvas DragDropCanvas;
        
        protected override BaseScreen InstantiateScreen(BaseScreen screenPrefab)
        {
            var newScreen = _diContainer.InstantiatePrefabForComponent<BaseScreen>(screenPrefab, _screenCanvas.transform);
            return newScreen;
        }
    }
}