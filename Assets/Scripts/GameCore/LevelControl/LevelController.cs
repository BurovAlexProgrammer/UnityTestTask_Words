﻿using System;
using System.Collections.Generic;
using System.Linq;
using GameCore.LevelControl.Views;
using GameCore.Models;
using Services;
using UnityEngine;
using Zenject;
using static GameCore.LevelControl.Views.ClusterPlaceholderView.State;

namespace GameCore.LevelControl
{
    public class LevelController : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private SessionResultService _sessionResultService;

        [SerializeField] private Transform _wordsContainer;
        [SerializeField] private Transform _clustersContainer;
        [SerializeField] private WordPlaceholderView _wordPlaceholderPrefab;
        [SerializeField] private ClusterView _clusterPrefab;

        public event Action<LevelData> Finished;

        private readonly List<WordPlaceholderView> _wordPlaceholders = new();
        private string[] _words;
        private LevelData _levelData;

        public void Init(LevelData levelData)
        {
            _sessionResultService.Init();
            _levelData = levelData;

            for (var i = 0; i < levelData.Words.Length; i++)
            {
                var wordPlaceholder = _diContainer.InstantiatePrefabForComponent<WordPlaceholderView>(_wordPlaceholderPrefab, _wordsContainer);
                wordPlaceholder.Init(levelData.Words[i]);
                wordPlaceholder.Changed += OnWordPlaceholderChanged;
                _wordPlaceholders.Add(wordPlaceholder);
                _words = _levelData.Words.Select(x => x.Text).ToArray();
            }

            var clusters = _levelData.Words.SelectMany(x => x.Clusters).ToArray();

            for (var i = 0; i < clusters.Count(); i++)
            {
                var cluster = _diContainer.InstantiatePrefabForComponent<ClusterView>(_clusterPrefab, _clustersContainer);
                cluster.Init(clusters[i]);
                cluster.DroppedToPlaceholder += OnClusterDroppedToPlaceholder;
                cluster.DroppedToPanel += ClusterOnDroppedToPanel;
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void ClusterOnDroppedToPanel(ClusterView clusterView, ClusterPlaceholderView placeholderView)
        {
            placeholderView.Detach(clusterView);
            clusterView.PlaceTo(_clustersContainer);
        }

        private void OnClusterDroppedToPlaceholder(ClusterView clusterView, ClusterPlaceholderView placeholderView)
        {
            if (clusterView.PrevClusterPlaceholder != null && clusterView.PrevClusterPlaceholder != placeholderView)
                clusterView.PrevClusterPlaceholder.Detach(clusterView);

            placeholderView.Attach(clusterView);
            clusterView.PlaceTo(placeholderView.transform);
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

            if (_wordPlaceholders.All(x => x.CurrentState == Right))
            {
                _wordPlaceholders.ForEach(x => _sessionResultService.AddClaimedWord(x.ResultWord));
                FinishLevel();
            }
        }

        private void FinishLevel()
        {
            Destroy(gameObject);
            Finished?.Invoke(_levelData);
        }
    }
}