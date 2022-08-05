using System.Linq;
using TapSwap.Runtime.App;
using TapSwap.UI;
using UnityEngine;
using UnityEngine.UI;
using Screen = TapSwap.UI.Screen;

namespace TapSwap.Managers.UI
{
    public class Router : MonoBehaviour, IRouter
    {
        [SerializeField] private Screen[] _screens;
        [SerializeField] private GameObject screenContainer;
        [Space]
        [SerializeField] private PlayerInfo _playerInfo;
        [SerializeField] private ButtonsContainer _buttonsContainer;
        [SerializeField] private Image _backGround;

        private Screen _currentScreen;

        private void Awake()
        {
            screenContainer.SetActive(false);
            
            DI.Add<IRouter>(this);
        }
        
        public Screen CurrentScreen => _currentScreen;

        public PlayerInfo PlayerInfo => _playerInfo;
        public ButtonsContainer ButtonsContainer => _buttonsContainer;

        public void Init()
        {
            screenContainer.SetActive(true);
        }

        public void ShowScreen(ScreenType type)
        {
            var screen = _screens.FirstOrDefault(x => x.Type == type);
            
            if (screen == null) return;
            
            _currentScreen = screen;
            screen.Show();
        }

        public void HideScreen(ScreenType type)
        {
            var screen = _screens.FirstOrDefault(x => x.Type == type);
            
            if (screen == null) return;
            
            screen.Hide();
        }

        public void HideCurrentScreen()
        {
            HideScreen(_currentScreen.Type);
        }
        
        public void HideGameElements()
        {
            _backGround.enabled = true;
        }

        public void ShowGameElements()
        {
            _backGround.enabled = false;
        }
    }
}