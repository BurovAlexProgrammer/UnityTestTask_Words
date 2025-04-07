using System;
using AppCoreModule.Scripts.Extensions;
using MyBox;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.LevelControl.Views
{
    public class ClusterPlaceholderView : MonoBehaviour
    {
        [SerializeField, ReadOnly] private ClusterView _attachedCluster;
        [SerializeField] private Image _highlight;

        private static int _index;
        private static int GetNewIndex() => ++_index;
        
        private readonly Color RightColor = new Color().FromHex("118B1D");
        private readonly Color WrongColor = new Color().FromHex("972112");
        
        public State CurrentState { get; private set; }
        
        public event Action Changed;
        
        public bool HasCluster => _attachedCluster != default;
        public string ClusterValue => _attachedCluster == null ? null : _attachedCluster.Cluster.Letters;

        private void Start()
        {
            gameObject.name = $"ClusterPlaceholder_{GetNewIndex().ToString()}";
            SetState(State.Default);
        }

        public void Attach(ClusterView clusterView)
        {
            _attachedCluster = clusterView;
            Changed?.Invoke();
        }

        public void Detach(ClusterView clusterView)
        {
            if (clusterView != _attachedCluster)
            {
                Debug.LogError("ClusterView: wrong clusterView.");
                return;
            }
            
            _attachedCluster = null;
            Changed?.Invoke();
        }

        public void SetState(State state)
        {
            CurrentState = state;
            _highlight.enabled = CurrentState != State.Default;
            _highlight.color = CurrentState == State.Right ? RightColor : WrongColor;
        }

        public enum State
        {
            Default,
            Wrong,
            Right
        }
    }
}