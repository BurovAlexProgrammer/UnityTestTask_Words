using System.Security.Cryptography.X509Certificates;
using AppCoreModule.Scripts.UI.Screens;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using ResourceProvider = Services.ResourceProvider;

namespace UI.Screens
{
    public class ResultScreen : BaseScreen
    {
        [Inject] private SessionResultService _sessionResultService;
        [Inject] private ScreenService _screenService;
        [Inject] private AudioService _audioService;

        [SerializeField] private Transform _wordsTextsContainer;
        [SerializeField] private TextMeshProUGUI _wordTextPrefab;
        [SerializeField] private TextMeshProUGUI _wordsCountText;
        [SerializeField] private Button _buttonMainMenu;
        [SerializeField] private Button _buttonAgain;
        
        protected override void Init()
        {
            base.Init();
            _buttonMainMenu.onClick.AddListener(() => OnMainMenuClicked().Forget());
            _buttonAgain.onClick.AddListener(() => OnAgainClicked().Forget());

            var claimedWords = _sessionResultService.ClaimedWords();
            var index = 0;

            DOVirtual.Int(0, claimedWords.Length, 3f, x =>
            {
                _wordsCountText.text = x.ToString();
                
                if (x > claimedWords.Length || index == x) return;

                index = x;
                var wordView = Instantiate(_wordTextPrefab, _wordsTextsContainer);
                wordView.text = claimedWords[x - 1];
                DoFadeIn(wordView.transform);
                DoFadeIn(_wordsCountText.transform, Vector3.one * 0.2f);
                _audioService.PlayBoolk();
            }).SetDelay(0.5f);
        }

        private async UniTask OnMainMenuClicked()
        {
            var prefab = await ResourceProvider.GetScreenPrefabAsync(ResourceProvider.ScreenNames.MainMenuScreen);
            _screenService.OpenScreen(prefab, true);
        }
        
        private async UniTask OnAgainClicked()
        {
            var prefab = await ResourceProvider.GetScreenPrefabAsync(ResourceProvider.ScreenNames.GameScreen);
            _screenService.OpenScreen(prefab, true);
        }

        private Tween DoFadeIn(Transform trans, Vector3 startScale = new())
        {
            trans.localScale = startScale;
            return trans.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
        }
    }
}