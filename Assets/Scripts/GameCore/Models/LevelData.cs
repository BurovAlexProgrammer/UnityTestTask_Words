using System;
using System.Collections.Generic;

namespace GameCore.Models
{
    //For level generator
    [Serializable]
    public struct LevelConfigs
    {
        public List<LevelData> Levels;
    }
    
    [Serializable]
    public struct LevelData
    {
        public int Level;
        public Word[] Words;
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