using System.Linq;
using TapSwap.Managers.Game;
using TapSwap.Managers.Health;
using TapSwap.Managers.Score;
using TapSwap.Runtime.App;
using UnityEngine;
using UnityEngine.UI;

namespace TapSwap.UI.Screens
{
    public class MainGame : Screen
    {
        [SerializeField] private GameObject[] _hearts;
        [SerializeField] private Button _pause;
        [SerializeField] private Text _score;

        private IHealthManager _healthManager;
        private IScoreManager _scoreManager;
        private IGameManager _gameManager;

        private void Start()
        {
            _healthManager = DI.Get<IHealthManager>();
            _scoreManager = DI.Get<IScoreManager>();
            _gameManager = DI.Get<IGameManager>();

            _pause.onClick.AddListener(_gameManager.Pause);
            
            _healthManager.HealthIncrease += OnHealthIncreased;
            _healthManager.HealthDecrease += OnHealthDecreased;

            _scoreManager.ScoreChanged += OnScoreChanged;
        }
        
        protected override void Init()
        {
            base.Init();

            UpdateHealthView();
        }

        private void UpdateHealthView()
        {
            foreach (var heart in _hearts)
            {
                heart.SetActive(false);
            }

            for (var i = 0; i < _healthManager.CurrentHealth; i++)
            {
                _hearts[i].SetActive(true);
            }
        }

        private void OnScoreChanged(int score)
        {
            _score.text = $"{score}";
        }

        private void OnHealthDecreased()
        {
            var heart = _hearts.LastOrDefault(x => x.activeSelf);
            
            if (heart == null) return; 
            
            heart.SetActive(false);
        }

        private void OnHealthIncreased()
        {
            var heart = _hearts.FirstOrDefault(x => !x.activeSelf);
            
            if (heart == null) return; 
            
            heart.SetActive(true);
        }


        public override ScreenType Type => ScreenType.MainGame;
    }
}