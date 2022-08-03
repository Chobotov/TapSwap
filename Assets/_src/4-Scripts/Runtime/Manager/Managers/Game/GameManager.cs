using TapSwap.Managers.UI;
using TapSwap.Runtime.App;
using TapSwap.UI;
using UnityEngine;

namespace TapSwap.Managers.Game
{
    public class GameManager : IGameManager
    {
        private IRouter _router;

        public GameManager()
        {
            _router = DI.Get<IRouter>();
        }
        
        public void StartSession()
        {
            _router.ShowScreen(ScreenType.StartScreen);
        }

        public void Start()
        {
            _router.HideCurrentScreen();
            _router.ShowScreen(ScreenType.GameScreen);
        }

        public void Pause()
        {
            _router.HideCurrentScreen();
            _router.ShowScreen(ScreenType.PauseScreen);
        }

        public void Resume()
        {
            
        }

        public void Restart()
        {
            
        }

        public void Exit()
        {
            PlayerPrefs.Save();
        }
    }
}