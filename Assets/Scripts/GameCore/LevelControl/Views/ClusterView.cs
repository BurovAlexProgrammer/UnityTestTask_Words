using GameCore.Models;
using TMPro;
using UnityEngine;

namespace GameCore.LevelControl.Views
{
    public class ClusterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textView;
        
        public Cluster Cluster { get; private set; }

        public void Init(Cluster cluster)
        {
            Cluster = cluster;
        }
    }
}