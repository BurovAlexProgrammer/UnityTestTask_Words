using System;
using MyBox;
using UnityEngine;

namespace GameCore.LevelControl.Views
{
    public class ClusterPlaceholderView : MonoBehaviour
    {
        [SerializeField, ReadOnly] private ClusterView _attachedCluster;
        
        public State CurrentState { get; private set; }
        
        public event Action Changed;

        public bool HasCluster => _attachedCluster != default;
        public string ClusterValue => _attachedCluster == null ? null : _attachedCluster.Cluster.Letters;

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
        }

        public enum State
        {
            Default,
            Wrong,
            Right
        }
    }
}