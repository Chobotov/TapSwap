using TapSwap.Managers.Game;
using TapSwap.Runtime.App;
using UnityEngine;
using UnityEngine.UI;

namespace TapSwap.UI.Screens
{
    public class TapToPlay : Screen
    {
        [SerializeField] private Button _start;

        private IGameManager _gameManager;
        
        private IGameManager GameManager
        {
            get
            {
                if (_gameManager == null)
                {
                    _gameManager = DI.Get<IGameManager>();
                }

                return _gameManager;
            }
        }

        private void OnEnable()
        {
            _start.onClick.AddListener(GameManager.Start);
        }

        private void OnDisable()
        {
            _start.onClick.RemoveAllListeners();
        }

        public override ScreenType Type => ScreenType.StartScreen;
    }
}