using System;
using System.Linq;
using System.Text;
using GameCore.Models;
using MyBox;
using UnityEngine;
using Zenject;

namespace GameCore.LevelControl.Views
{
    public class WordPlaceholderView : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;

        [SerializeField] private ClusterPlaceholderView _clusterPlaceholderPrefab;
        [SerializeField, ReadOnly] private ClusterPlaceholderView[] _clusterPlaceholders;
        
        private ClusterPlaceholderView.State _currentState;
        private StringBuilder _stringBuilder = new();
        private Transform _transform;
        private Word _word;

        public event Action<WordPlaceholderView> Changed;

        public string ResultWord => _stringBuilder.ToString();
        public bool HasEmptyClusters => _clusterPlaceholders.Any(x => x.HasCluster == false);
        public ClusterPlaceholderView.State CurrentState => _currentState;


        public void Init(Word word)
        {
            _word = word;
            _transform = transform;
            _clusterPlaceholders = new ClusterPlaceholderView[word.Clusters.Length];

            for (var i = 0; i < _clusterPlaceholders.Length; i++)
            {
                _clusterPlaceholders[i] = Instantiate(_clusterPlaceholderPrefab, _transform);
                _diContainer.Inject(_clusterPlaceholders[i]);
                // _clusterPlaceholders[i].Init();
                _clusterPlaceholders[i].Changed += OnClusterViewChanged;
            }
        }

        public void SetClustersState(ClusterPlaceholderView.State state)
        {
            _currentState = state;
            foreach (var clusterPlaceholder in _clusterPlaceholders)
            {
                clusterPlaceholder.SetState(state);
            }
        }

        private void OnClusterViewChanged()
        {
            _stringBuilder.Clear();
            
            foreach (var clusterPlaceholder in _clusterPlaceholders)
            {
                _stringBuilder.Append(clusterPlaceholder.ClusterValue);
            }

            Changed?.Invoke(this);
        }
    }
}