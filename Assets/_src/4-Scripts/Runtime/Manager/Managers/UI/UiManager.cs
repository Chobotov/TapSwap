using System.Linq;
using TapSwap.UI;
using UnityEngine;
using Screen = TapSwap.UI.Screen;

namespace TapSwap.Scripts.Managers
{
    public class UiManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private Screen[] _screens;

        private Screen _currentScreen;

        public Screen CurrentScreen => _currentScreen;

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
    }
}