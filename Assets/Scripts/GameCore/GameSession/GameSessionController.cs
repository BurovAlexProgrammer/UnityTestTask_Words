using System;
using Cysharp.Threading.Tasks;
using GameCore.LevelControl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Services
{
    public class GameSessionController : MonoBehaviour
    {
        [Inject] private ScreenService _screenService;
        [Inject] private LevelProvider _levelProvider;
        [Inject] private LevelController _levelController;
        
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Button _mainMenuButton;

        private int _currentLevel = 1;
        
        public void Start()
        {
            IniAsync().Forget();
        }

        private void OnDestroy()
        {
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        }

        private async UniTask IniAsync()
        {
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            var level = await _levelProvider.GetLevel(_currentLevel);
            _levelController.Init(level);
        }

        private void OnMainMenuButtonClicked()
        {
            _screenService.GoBack();
        }
    }
}