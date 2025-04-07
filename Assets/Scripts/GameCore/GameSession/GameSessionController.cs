using System;
using Cysharp.Threading.Tasks;
using GameCore.LevelControl;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Services
{
    public class GameSessionController : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private ScreenService _screenService;
        [Inject] private LevelProvider _levelProvider;
        
        [SerializeField] private LevelController _levelControllerPrefab;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Button _mainMenuButton;

        private LevelController _currentLevelController;
        private int _currentLevel;
        
        public void Start()
        {
            InitAsync().Forget();
        }

        private void OnDestroy()
        {
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        }

        private async UniTask InitAsync()
        {
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            StartLevelAsync().Forget();
        }

        private void OnLevelFinished()
        {
            StartLevelAsync().Forget();
        }

        private async UniTask StartLevelAsync()
        {
            _currentLevel++;
            _currentLevelController = _diContainer.InstantiatePrefabForComponent<LevelController>(_levelControllerPrefab);
            _currentLevelController.Finished += OnLevelFinished;
            var levelData = await _levelProvider.GetLevel(_currentLevel);
            _currentLevelController.Init(levelData);
        }

        private void OnMainMenuButtonClicked()
        {
            _screenService.GoBack();
        }
    }
}