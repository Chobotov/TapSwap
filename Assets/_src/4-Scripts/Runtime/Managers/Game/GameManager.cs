using TapSwap.DropItem;
using TapSwap.Managers.Health;
using TapSwap.Managers.Score;
using TapSwap.Managers.UI;
using TapSwap.Runtime.App;
using TapSwap.UI;
using UnityEngine;

namespace TapSwap.Managers.Game
{
    public class GameManager : IGameManager
    {
        private IRouter _router;
        private IHealthManager _healthManager;
        private IScoreManager _scoreManager;

        public GameManager()
        {
            _router = DI.Get<IRouter>();
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
            _router.HideCurrentScreen();
            _router.ShowScreen(ScreenType.Pause);
            
            GameState.SwitchTo(GameState.State.Pause);
        }

        public void Resume()
        {
            _router.HideCurrentScreen();
            _router.ShowScreen(ScreenType.MainGame);
            
            DI.Get<GameTimer>().StartTimer(() =>
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

        public void Exit()
        {
            PlayerPrefs.Save();
        }
    }
}