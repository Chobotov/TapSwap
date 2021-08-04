using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TapSwap
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private RectTransform[] _hearts;
        [SerializeField] private Transform _background;

        [SerializeField] private SpawnItems _spawn;
        
        [SerializeField] private AudioSource _gameAudio;
        [SerializeField] private AudioClip _gameOver;
        
        [SerializeField] private Transform _main;
        [SerializeField] private Transform _game;
        [SerializeField] private Transform _heal;
        [SerializeField] private Transform _pipes;
        [SerializeField] private Transform _text;
        [SerializeField] private Transform _lines;
        
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _pauseButton;
        
        [SerializeField] private Text _currentScore;
        [SerializeField] private Text _recordScore;
        [SerializeField] private Text _timer;

        [SerializeField] private Transform _buttons;

        private Score _score;
        private ControlPipes _controlPipes;

        private int _heartNum;
        private int _scoreValue;
        private string _isSound = "on";
        
        private IEnumerator _spawnItems;

        public int HeartNum => _heartNum;
        
        public void IncreaseHeartNum(int value = 1) => _heartNum += 1;
        public void DecreaseHeartNum(int value = 1) => _heartNum -= 1;
        
        public void StartGame()
        {
            _pauseButton.gameObject.SetActive(true);
            _main.gameObject.SetActive(false);
            _pipes.gameObject.SetActive(true);
            _heal.gameObject.SetActive(true);
            _game.gameObject.SetActive(true);
            _background.gameObject.SetActive(false);
            
            StartCoroutine(_spawnItems);
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            
            _heal.gameObject.SetActive(false);
            _pipes.gameObject.SetActive(false);
            _game.gameObject.SetActive(false);
            _pauseButton.gameObject.SetActive(false);
            _background.gameObject.SetActive(true);
            _text.gameObject.SetActive(true);
            _buttons.gameObject.SetActive(true);
            _currentScore.text = $"{_score.Value}";
            _recordScore.text = $"{PlayerPrefs.GetInt("Record")}";
        }

        public void Resume()
        {
            StartCoroutine(ResumeGame());
            
            _background.gameObject.SetActive(false);
            _heal.gameObject.SetActive(true);
            _game.gameObject.SetActive(true);
            _text.gameObject.SetActive(false);
            _buttons.gameObject.SetActive(false);
        }

        public void Restart()
        {
            PlayerPrefs.Save();
            SceneManager.LoadScene(0);
        }

        public void SoundOnOff()
        {
            if (PlayerPrefs.GetString("Sound").Equals("on"))
            {
                _lines.gameObject.SetActive(false);
                _isSound = "off";
                PlayerPrefs.SetString("Sound", _isSound);
                _gameAudio.volume = 0f;
            }
            else
            {
                _lines.gameObject.SetActive(true);
                _isSound = "on";
                PlayerPrefs.SetString("Sound", _isSound);
                _gameAudio.volume = 1f;
            }
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
        
        public void ShowHealth(int heartNum, bool show)
        {
            for (var i = 0; i <= 2; i++) _hearts[i].gameObject.SetActive(false);

            for (var i = 0; i <= heartNum; i++) _hearts[i].gameObject.SetActive(show);
        }
        
        private void Awake()
        {
            var player = Camera.main;
            
            _score = GetComponent<Score>();
            _gameAudio = player.GetComponent<AudioSource>();
            _controlPipes = player.GetComponent<ControlPipes>();
            
            if (PlayerPrefs.GetString("Sound").Equals("on"))
            {
                _lines.gameObject.SetActive(true);
                _gameAudio.volume = 1f;
            }
            else
            {
                _lines.gameObject.SetActive(false);
                _gameAudio.volume = 0f;
            }
        }

        private void Start()
        {
            Time.timeScale = 1f;
            
            _heartNum = 2;
            _isSound = PlayerPrefs.GetString("Sound");
            _spawnItems = ItemSpawn();
        }

        private void Update()
        {
            _controlPipes.SelectPipe();
            
            if (_score.Value < 0 || _heartNum < 0) GameOver();
        }

        private void GameOver()
        {
            _gameAudio.PlayOneShot(_gameOver);

            StopCoroutine(_spawnItems);

            _background.gameObject.SetActive(true);
            _main.gameObject.SetActive(false);
            _pipes.gameObject.SetActive(false);
            _heal.gameObject.SetActive(false);
            _game.gameObject.SetActive(false);
            _text.gameObject.SetActive(true);
            _buttons.gameObject.SetActive(true);
            _pauseButton.gameObject.SetActive(false);
            _resumeButton.gameObject.SetActive(false);
            _restartButton.gameObject.SetActive(true);

            if (_score.Value < 0)
            {
                _score.ClearScore();
                _scoreValue = 0;
            }
            else _scoreValue = _score.Value;

            _currentScore.text = $"{_score.Value}";
            _score.ClearScore();
            _heartNum = 2;
            _recordScore.text = $"{PlayerPrefs.GetInt("Record")}";
        }

        private IEnumerator ItemSpawn()
        {
            while (_score.Value >= 0)
            {
                _spawn.Spawn();
                
                yield return new WaitForSeconds(_score.Speed);
            }
        }

        private IEnumerator ResumeGame()
        {
            var num = 3;
            _timer.gameObject.SetActive(true);
            
            for (var i = 0; i < 3; i++)
            {
                _timer.text = $"{num}";
                num -= 1;
                
                yield return new WaitForSecondsRealtime(1);
            }

            _timer.gameObject.SetActive(false);
            _pipes.gameObject.SetActive(true);
            _pauseButton.gameObject.SetActive(true);
            Time.timeScale = 1f;
        }
    }
}
