using System;

namespace GameCore.Models
{
    [Serializable]
    public struct LevelData
    {
        public int Level;
        public Word[] Word;
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