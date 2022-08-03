using TapSwap.Managers.Audio;
using TapSwap.Managers.Game;
using TapSwap.Runtime.App;
using UnityEngine;
using UnityEngine.UI;

namespace TapSwap.UI.Screens
{
    public class Pause : Screen
    {
        [SerializeField] private Toggle _audio;
        [SerializeField] private Button _resume;
        [SerializeField] private Button _exit;

        private IGameManager _gameManager;
        private IAudioManager _audioManager;

        private void Start()
        {
            _gameManager = DI.Get<IGameManager>();
            _audioManager = DI.Get<IAudioManager>();
            
            _resume.onClick.AddListener(_gameManager.Resume);
            _exit.onClick.AddListener(_gameManager.Pause);
            
            _audio.onValueChanged.AddListener(_audioManager.SetAudioState);
        }

        public override ScreenType Type => ScreenType.PauseScreen;
    }
}