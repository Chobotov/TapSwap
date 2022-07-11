using TapSwap.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace TapSwap.UI.Screens
{
    public class StartScreen : Screen
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private Button _start;
        
        private void OnEnable()
        {
            _start.onClick.AddListener(_gameManager.StartGame);
        }

        private void OnDisable()
        {
            _start.onClick.RemoveAllListeners();
        }

        public override ScreenType Type => ScreenType.StartScreen;
    }
}