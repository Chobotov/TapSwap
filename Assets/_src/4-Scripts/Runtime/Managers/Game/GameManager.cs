using TapSwap.Managers.Health;
using TapSwap.Managers.Score;
using TapSwap.Managers.UI;
using TapSwap.Runtime.App;
using TapSwap.UI;
using TapSwap.Utils;
using UnityEngine;

namespace TapSwap.Managers.Game
{
    public class GameManager : IGameManager
    {
        private IRouter _router;
        private IHealthManager _healthManager;
        private IScoreManager _scoreManager;

        private GameTimer _gameTimer;

        public GameManager(IRouter router, IHealthManager healthManager, IScoreManager scoreManager)
        {
            _router = router;
            _healthManager = healthManager;
            _scoreManager = scoreManager;

            _gameTimer = DI.Get<GameTimer>();
            
            GameState.GameStateChange += state =>
            {
                if (state != GameState.State.GameOver) return; 
                
                GameOver();
            };
        }
        
        public void StartSession()
        {
            _router.ShowScreen(ScreenType.TapToPlay);
        }

        public void Start()
        {
            _router.HideCurrentScreen();
            _router.ShowScreen(ScreenType.MainGame);
            
            GameState.SwitchTo(GameState.State.Game);

            _ = new GameSession();
        }

        public void Pause()
        {
            _gameTimer.StopTimer();
            
            _router.HideCurrentScreen();
            _router.ShowScreen(ScreenType.Pause);
            
            _router.PlayerInfo.RecordScore.text = $"{_scoreManager.RecordScore}";
            _router.PlayerInfo.CurrentScore.text = $"{_scoreManager.CurrentScore}";
            _router.PlayerInfo.Show();
            
            _router.ButtonsContainer.Show();
            
            _router.ButtonsContainer.Restart.gameObject.SetActive(false);
            _router.ButtonsContainer.Resume.gameObject.SetActive(true);
            
            GameState.SwitchTo(GameState.State.Pause);
        }

        public void Resume()
        {
            _router.HideCurrentScreen();
            _router.ShowScreen(ScreenType.MainGame);
            
            _gameTimer.StartTimer(() =>
            {
                GameState.SwitchTo(GameState.State.Game);
            });
        }

        public void Restart()
        {
            _router.HideCurrentScreen();
            _router.ShowScreen(ScreenType.TapToPlay);
            
            _scoreManager.Reset();
            _healthManager.Reset();
        }

        public void GameOver()
        {
            _router.HideCurrentScreen();
            _router.ShowScreen(ScreenType.GameOver);
            
            _router.PlayerInfo.RecordScore.text = $"{_scoreManager.RecordScore}";
            _router.PlayerInfo.CurrentScore.text = $"{_scoreManager.CurrentScore}";
            _router.PlayerInfo.Show();
            
            _router.ButtonsContainer.Show();
            
            _router.ButtonsContainer.Restart.gameObject.SetActive(true);
            _router.ButtonsContainer.Resume.gameObject.SetActive(false);
        }

        public void Exit()
        {
            PlayerPrefs.Save();
        }
    }
}