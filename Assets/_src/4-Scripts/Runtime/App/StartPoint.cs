using TapSwap.Managers.Audio;
using TapSwap.Managers.Game;
using TapSwap.Managers.Health;
using TapSwap.Managers.Score;
using TapSwap.Managers.Speed;
using TapSwap.Managers.UI;
using UnityEngine;

namespace TapSwap.Runtime.App
{
    public sealed class StartPoint
    {
        private IGameManager _gameManager;
        private IAudioManager _audioManager;
        private IHealthManager _healthManager;
        private IScoreManager _scoreManager;
        private ISpeedManager _speedManager;
        
        private StartPoint()
        {
            InitManagers();       
            
            _gameManager.StartSession();
        }

        private void InitManagers()
        {
            var router = DI.Get<IRouter>();
            
            _audioManager = new AudioManager();
            DI.Add<IAudioManager>(_audioManager);

            _healthManager = new HealthManager();
            DI.Add<IHealthManager>(_healthManager);

            _scoreManager = new ScoreManager();
            DI.Add<IScoreManager>(_scoreManager);

            _speedManager = new SpeedManager(_scoreManager);
            DI.Add<ISpeedManager>(_speedManager);
            
            _gameManager = new GameManager(router, _healthManager, _scoreManager);
            DI.Add<IGameManager>(_gameManager);

            Debug.Log("Managers Inited!");
            
            router.Init();

            Debug.Log("Router Inited!");
        }
        
        [RuntimeInitializeOnLoadMethod]
        private static void InitGame()
        {
            Debug.Log("Init Game!");
            
            _ = new StartPoint();
        }
    }
}