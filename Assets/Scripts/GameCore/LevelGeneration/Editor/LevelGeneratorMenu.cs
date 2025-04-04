#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GameCore.LevelGeneration.Editor
        {
        public static class LevelGeneratorMenu
        {
            [MenuItem("Tools/Generate Level Files")]
            public static void GenerateLevelFilesMenu()
            {
                LevelGenerator.GenerateFiles();
                Debug.Log("Level files generated successfully!");
                AssetDatabase.Refresh();
            }
        }
    }
#endif
