using System.Linq;
using GameCore.LevelControl.Views;
using GameCore.Models;
using UnityEngine;
using static GameCore.LevelControl.Views.ClusterPlaceholderView.State;

namespace GameCore.LevelControl
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private Transform _wordsContainer;
        [SerializeField] private WordPlaceholderView _wordPlaceholderPrefab;
        
        private LevelData _levelData;
        private string[] _words;

        public void Init(LevelData levelData)
        {
            _levelData = levelData;
            for (var i = 0; i < levelData.Words.Length; i++)
            {
                var wordPlaceholder = Instantiate(_wordPlaceholderPrefab, _wordsContainer);
                wordPlaceholder.Init(levelData.Words[i]);
                wordPlaceholder.Changed += OnWordPlaceholderChanged;
                _words = _levelData.Words.Select(x => x.Text).ToArray();
            }
        }

        private void OnWordPlaceholderChanged(WordPlaceholderView wordPlaceholder)
        {
            if (wordPlaceholder.HasEmptyClusters)
            {
                wordPlaceholder.SetClustersState(Default);
            }
            else
            {
                var isRightWord = _words.Contains(wordPlaceholder.ResultWord);
                wordPlaceholder.SetClustersState(isRightWord ? Right : Wrong);
            }
        }
    }
}