using TapSwap.Managers.Game;
using TapSwap.Managers.Score;
using TapSwap.Runtime.App;
using UnityEngine;
using UnityEngine.UI;

namespace TapSwap.UI.Screens
{
    public class GameOver : Screen
    {
        [SerializeField] private Text _recordScore;
        [SerializeField] private Text _currentScore;
        [Space]
        [SerializeField] private Button _restart;

        private IGameManager _gameManager;
        private IScoreManager _scoreManager;
        
        private void Start()
        {
            _gameManager = DI.Get<IGameManager>();
            _scoreManager = DI.Get<IScoreManager>();

            _recordScore.text = $"{_scoreManager.RecordScore}";
            _currentScore.text = $"{_scoreManager.CurrentScore}";
            
            _restart.onClick.AddListener(_gameManager.Restart);
        }

        public override ScreenType Type => ScreenType.GameOverScreen;
    }
}