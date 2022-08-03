using TapSwap.Managers.Audio;
using TapSwap.Managers.Game;
using TapSwap.Runtime.App;
using UnityEngine;

namespace TapSwap.UI.Screens
{
    public class Pause : Screen
    {
        [SerializeField] private ButtonsContainer _buttonsContainer;

        private IGameManager _gameManager;
        private IAudioManager _audioManager;

        private void Start()
        {
            _gameManager = DI.Get<IGameManager>();
            _audioManager = DI.Get<IAudioManager>();
            
            _buttonsContainer.Resume.onClick.AddListener(_gameManager.Resume);
            _buttonsContainer.Resume.onClick.AddListener(_buttonsContainer.HideButtons);
            _buttonsContainer.Exit.onClick.AddListener(_gameManager.Pause);
            
            _buttonsContainer.Audio.onValueChanged.AddListener(_audioManager.SetAudioState);
        }
        
        protected override void Init()
        {
            base.Init();
            
            _buttonsContainer.ShowButtons();
            
            _buttonsContainer.Restart.gameObject.SetActive(false);
            _buttonsContainer.Resume.gameObject.SetActive(true);
            
        }

        public override ScreenType Type => ScreenType.Pause;
    }
}