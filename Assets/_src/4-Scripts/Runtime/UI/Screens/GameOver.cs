using TapSwap.Managers.Game;
using TapSwap.Managers.Score;
using TapSwap.Managers.UI;
using TapSwap.Runtime.App;
using UnityEngine;
using UnityEngine.UI;

namespace TapSwap.UI.Screens
{
    public class GameOver : Screen
    {
        [SerializeField] private Text _recordScore;
        [SerializeField] private Text _currentScore;

        private IGameManager _gameManager;
        private IScoreManager _scoreManager;
        
        private void Start()
        {
            var router = DI.Get<IRouter>();
            var buttonsContainer = router.ButtonsContainer;
            
            _gameManager = DI.Get<IGameManager>();
            _scoreManager = DI.Get<IScoreManager>();

            buttonsContainer.Restart.onClick.AddListener(_gameManager.Restart);
            buttonsContainer.Restart.onClick.AddListener(buttonsContainer.Hide);
            buttonsContainer.Restart.onClick.AddListener(router.PlayerInfo.Hide);
        }

        public override ScreenType Type => ScreenType.GameOver;
    }
}