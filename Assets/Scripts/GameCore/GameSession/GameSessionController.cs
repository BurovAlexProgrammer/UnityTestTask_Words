using Cysharp.Threading.Tasks;
using GameCore.LevelControl;
using GameCore.Models;
using TMPro;
using UnityEngine;
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
        private int _currentLevel = 3;
        
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
            await StartLevelAsync();
        }

        private void OnLevelFinished(LevelData levelData)
        {
            // _claimedWords.AddRange(levelData.Words.Select(x => x.Text).ToArray());
            StartLevelAsync().Forget();
        }

        private async UniTask StartLevelAsync()
        {
            _currentLevel++;
            _currentLevelController = _diContainer.InstantiatePrefabForComponent<LevelController>(_levelControllerPrefab);
            _currentLevelController.Finished += OnLevelFinished;

            if (_currentLevel <= LevelProvider.MaxLevel)
            {
                var levelData = await _levelProvider.GetLevel(_currentLevel);
                _currentLevelController.Init(levelData);
            }
            else
            {
                GameOver().Forget();
            }
        }

        private void OnMainMenuButtonClicked()
        {
            _screenService.GoBack();
        }

        private async UniTask GameOver()
        {
            var resultScreen = await ResourceProvider.GetScreenPrefabAsync(ResourceProvider.ScreenNames.ResultScreen);
            await _screenService.OpenScreenAsync(resultScreen, true);
        }
    }
}