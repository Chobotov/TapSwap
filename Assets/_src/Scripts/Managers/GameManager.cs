using System.Collections;
using TapSwap.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TapSwap.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UiManager _uiManager;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private HealthManager _healthManager;
        [SerializeField] private ScoreManager scoreManagerManager;

        [SerializeField] private GameTimer _gameTimer;
        
        [SerializeField] private SpawnItems _spawn;
        
        private ControlPipes _controlPipes;

        private IEnumerator _spawnItems;

        private void Awake()
        {
            var player = Camera.main;
            _controlPipes = player.GetComponent<ControlPipes>();
        }

        private void Start()
        {
            Time.timeScale = 1f;
            _spawnItems = ItemSpawn();
            
            _uiManager.ShowScreen(ScreenType.StartScreen);
        }

        private void Update()
        {
            _controlPipes.SelectPipe();
        }

        public IHealthManager HealthManager => _healthManager;

        public void StartGame()
        {
            _uiManager.ShowScreen(ScreenType.GameScreen);
            StartCoroutine(_spawnItems);
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            
            _uiManager.HideScreen(_uiManager.CurrentScreen.Type);
            _uiManager.ShowScreen(ScreenType.PauseScreen);
            
            //_currentScore.text = $"{_scoreManager.CurrentScore}";
            //_recordScore.text = $"{PlayerPrefs.GetInt("Record")}";
        }

        public void Resume()
        {
            _gameTimer.StartTimer(OnTimerEnd);
        }

        public void Restart()
        {
            PlayerPrefs.Save();
            SceneManager.LoadScene(0);
        }

        public void Exit()
        {
            PlayerPrefs.Save();
            Application.Quit();
        }

        public void UpdateRecord(int score)
        {
            var record = PlayerPrefs.GetInt("Record");
            
            if (score > record) PlayerPrefs.SetInt("Record", score);
        }

        private void GameOver()
        {
            StopCoroutine(_spawnItems);
            
            _audioManager.PlayGameOverSound();

            if (scoreManagerManager.CurrentScore < 0)
            {
                //_score.ClearScore();
                //_scoreValue = 0;
            }
            //else _scoreValue = _score.Value;

            /*_currentScore.text = $"{_score.Value}";
            _score.ClearScore();
            _heartNum = 2;
            _recordScore.text = $"{PlayerPrefs.GetInt("Record")}";*/
        }

        private IEnumerator ItemSpawn()
        {
            while (scoreManagerManager.CurrentScore >= 0)
            {
                _spawn.Spawn();
                
                yield return new WaitForSeconds(0f/*_score.Speed*/);
            }
        }

        private void OnEnable()
        {
            _healthManager.HealthDecrease += OnHealthDecreased;
            scoreManagerManager.ScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            _healthManager.HealthDecrease -= OnHealthDecreased;
            scoreManagerManager.ScoreChanged -= OnScoreChanged;
        }

        private void OnHealthDecreased()
        {
            if (_healthManager.CurrentHealth < 0) GameOver();
        }

        private void OnScoreChanged()
        {
            if (scoreManagerManager.CurrentScore < 0) GameOver();
        }
        
        private void OnTimerEnd()
        {
            _uiManager.HideScreen(_uiManager.CurrentScreen.Type);
            _uiManager.ShowScreen(ScreenType.GameScreen);
        }
    }
}
