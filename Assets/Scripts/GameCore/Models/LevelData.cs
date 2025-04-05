using System;
using UnityEngine.Serialization;

namespace GameCore.Models
{
    [Serializable]
    public struct LevelData
    {
        public int Level;
        [FormerlySerializedAs("Word")] public Word[] Words;
    }
    
    [Serializable]
    public struct Word
    {
        public string Text;
        public Cluster[] Clusters;
    }

    [Serializable]
    public struct Cluster
    {
        public string Letters;
    }
}