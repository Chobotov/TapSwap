using TapSwap.Managers.Audio;
using TapSwap.Managers.Game;
using TapSwap.Managers.UI;
using TapSwap.Runtime.App;

namespace TapSwap.UI.Screens
{
    public class Pause : Screen
    {
        private IGameManager _gameManager;
        private IAudioManager _audioManager;

        private void Start()
        {
            var router = DI.Get<IRouter>();
            var buttonsContainer = router.ButtonsContainer;
            
            _gameManager = DI.Get<IGameManager>();
            _audioManager = DI.Get<IAudioManager>();
            
            buttonsContainer.Resume.onClick.AddListener(_gameManager.Resume);
            buttonsContainer.Resume.onClick.AddListener(buttonsContainer.Hide);
            buttonsContainer.Resume.onClick.AddListener(router.PlayerInfo.Hide);
            
            buttonsContainer.Exit.onClick.AddListener(_gameManager.Pause);
            
            buttonsContainer.Audio.onValueChanged.AddListener(_audioManager.SetAudioState);
        }

        public override ScreenType Type => ScreenType.Pause;
    }
}