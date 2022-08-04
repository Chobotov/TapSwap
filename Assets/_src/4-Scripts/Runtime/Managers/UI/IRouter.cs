using TapSwap.UI;

namespace TapSwap.Managers.UI
{
    public interface IRouter
    {
        Screen CurrentScreen { get; }
        void Init();
        void ShowScreen(ScreenType type);
        void HideScreen(ScreenType type);
        void HideCurrentScreen();
        
        PlayerInfo PlayerInfo { get; }
        ButtonsContainer ButtonsContainer { get; }
    }
}