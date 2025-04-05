using TMPro;
using UnityEngine;
using Zenject;

namespace Services
{
    public class GameSessionService : MonoBehaviour, IInitializable
    {
        [Inject] private LevelProvider _levelProvider;
        
        [SerializeField] private TextMeshProUGUI _levelText;

        private int _currentLevel = 1;
        
        public void Initialize()
        {
            _levelProvider.GetLevel(_currentLevel);
        }
    }
}