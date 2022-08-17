using TapSwap.Game;
using TapSwap.Managers.Audio;
using TapSwap.Managers.Health;
using TapSwap.Managers.Score;
using TapSwap.Managers.Speed;
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
        private IAudioManager _audioManager;
        private ISpeedManager _speedManager;

        private AudioInitiator _audioInitiator;
        private GameTimer _gameTimer;

        private TapSwap.Game.Game _game;

        public GameManager(IRouter router, IHealthManager healthManager, IScoreManager scoreManager, IAudioManager audioManager, ISpeedManager speedManager)
        {
            _router = router;
            _healthManager = healthManager;
            _scoreManager = scoreManager;
            _audioManager = audioManager;
            _speedManager = speedManager;

            _audioInitiator = DI.Get<AudioInitiator>();
            _gameTimer = DI.Get<GameTimer>();
            
            _router.ButtonsContainer.Audio.onValueChanged.AddListener(_audioManager.SetAudioState);
            _router.ButtonsContainer.Restart.onClick.AddListener(Restart);
            _router.ButtonsContainer.Resume.onClick.AddListener(Resume);

            GameState.GameStateChange += state =>
            {
                if (state != GameState.State.GameOver) return; 
                
                GameOver();
            };

            _router.ButtonsContainer.Audio.isOn = _audioManager.IsAudioEnable;
        }
        
        public void StartSession()
        {
            _router.ShowScreen(ScreenType.TapToPlay);
            _router.HideGameElements();
        }

        public void Start()
        {
            _router.HideCurrentScreen();
            _router.ShowGameElements();
            _router.ShowScreen(ScreenType.MainGame);
            
            GameState.SwitchTo(GameState.State.Game);

            _game = new TapSwap.Game.Game(_scoreManager, _healthManager, _audioInitiator);
        }

        public void Pause()
        {
            _gameTimer.StopTimer();
            
            _router.HideCurrentScreen();
            _router.ShowScreen(ScreenType.Pause);
            
            _router.PlayerInfo.RecordScore.text = $"{_scoreManager.RecordScore}";
            _router.PlayerInfo.CurrentScore.text = $"{_scoreManager.CurrentScore}";
            _router.PlayerInfo.Show();
            
            _router.ButtonsContainer.Resume.interactable = true;
            _router.ButtonsContainer.Show();

            GameState.SwitchTo(GameState.State.Pause);
        }

        public void Resume()
        {
            _router.ButtonsContainer.Hide();
            _router.PlayerInfo.Hide();
            
            _router.HideCurrentScreen();
            _router.ShowScreen(ScreenType.MainGame);
            
            _gameTimer.StartTimer(() =>
            {
                GameState.SwitchTo(GameState.State.Game);
            });
        }

        public void Restart()
        {
            _router.ButtonsContainer.Hide();
            _router.PlayerInfo.Hide();
            
            _router.HideCurrentScreen();
            
            _gameTimer.StopTimer();
            _game.EndGame();
            
            StartSession();
            
            _scoreManager.Reset();
            _healthManager.Reset();
            _speedManager.Reset();
        }

        public void GameOver()
        {
            _router.HideCurrentScreen();
            _router.HideGameElements();
            _router.ShowScreen(ScreenType.GameOver);

            _router.PlayerInfo.RecordScore.text = $"{_scoreManager.RecordScore}";
            _router.PlayerInfo.CurrentScore.text = $"{_scoreManager.CurrentScore}";
            _router.PlayerInfo.Show();

            _router.ButtonsContainer.Resume.interactable = false;
            _router.ButtonsContainer.Show();
        }

        public void Exit()
        {
            PlayerPrefs.Save();
        }
    }
}