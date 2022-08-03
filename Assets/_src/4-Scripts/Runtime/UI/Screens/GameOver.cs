using TapSwap.Managers.Game;
using TapSwap.Managers.Score;
using TapSwap.Runtime.App;
using UnityEngine;
using UnityEngine.UI;

namespace TapSwap.UI.Screens
{
    public class GameOver : Screen
    {
        [SerializeField] private Text _recordScore;
        [SerializeField] private Text _currentScore;
        [Space]
        [SerializeField] private ButtonsContainer _buttonsContainer;

        private IGameManager _gameManager;
        private IScoreManager _scoreManager;
        
        private void Start()
        {
            _gameManager = DI.Get<IGameManager>();
            _scoreManager = DI.Get<IScoreManager>();

            _recordScore.text = $"{_scoreManager.RecordScore}";
            _currentScore.text = $"{_scoreManager.CurrentScore}";
            
            _buttonsContainer.Restart.onClick.AddListener(_gameManager.Restart);
            _buttonsContainer.Restart.onClick.AddListener(_buttonsContainer.HideButtons);
        }

        protected override void Init()
        {
            base.Init();
        
            _buttonsContainer.ShowButtons();
            
            _buttonsContainer.Restart.gameObject.SetActive(true);
            _buttonsContainer.Resume.gameObject.SetActive(false);
            
        }

        public override ScreenType Type => ScreenType.GameOver;
    }
}