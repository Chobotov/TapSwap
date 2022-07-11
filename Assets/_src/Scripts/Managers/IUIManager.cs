using TapSwap.UI;

namespace TapSwap.Scripts.Managers
{
    public interface IUIManager
    {
        Screen CurrentScreen { get; }
        void ShowScreen(ScreenType type);
        void HideScreen(ScreenType type);
    }
}