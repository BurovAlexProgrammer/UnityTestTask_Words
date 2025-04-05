using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameCore.Models;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameCore.LevelGeneration
{
    public static class LevelGenerator
    {
        private static readonly string[] _mockup =
        {
            "звезда", "пейзаж", "пудель", "метель", "солнце", "фонарь", "сирень", "лопата", "остров", "банкет",
            "ворота", "гитара", "гранат", "дорога", "жемчуг", "камень", "кнопка", "колено", "корона", "лисица",
            "молоко", "огурец", "пальто", "ракета", "солнце", "улитка", "фонарь", "хоккей", "береза"
        };
        private const int WordsCount = 4;
        
        public static void GenerateFiles()
        {
            var dicIndex = 0; 
                
            for (var i = 0; i < 4; i++)
            {
                var path = Path.Combine(Application.dataPath, "Data", "Levels");
                var levelJson = CreateLevelJson(i + 1, dicIndex, WordsCount);
                var filePath = Path.Combine(path, $"level{i + 1}.json");
                File.WriteAllText(filePath, levelJson);
                dicIndex += WordsCount;
            }
        }

        private static string CreateLevelJson(int level, int startIndex, int wordsCount)
        {
            var levelData = new LevelData()
            {
                Level = level,
                Words = Enumerable.Range(0, wordsCount)
                    .Select(i => new Word
                    {
                        Text = _mockup[startIndex + i],
                        Clusters = SplitWord(_mockup[startIndex + i]).ToArray()
                    })
                    .ToArray()
            };

            return JsonUtility.ToJson(levelData);
        }

        static IEnumerable<Cluster> SplitWord(string word)
        {
            var i = 0;

            while (i < word.Length)
            {
                var clusterLength = Random.Range(2, 5);

                //Prevent one word in next segment
                if (clusterLength + i > word.Length - 2 && clusterLength < 3) 
                    clusterLength += 2;
                
                if (i + clusterLength > word.Length) 
                    clusterLength = word.Length - i;

                yield return new Cluster { Letters = word.Substring(i, clusterLength) };
                
                i += clusterLength;
            }
        }
    }
}