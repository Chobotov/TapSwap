using System.Collections;
using TapSwap.DropItem;
using TapSwap.Managers.Health;
using TapSwap.Managers.Score;
using TapSwap.Pipe;
using TapSwap.Runtime.App;
using UnityEngine;

namespace TapSwap.Game
{
    public class Game
    {
        private IScoreManager _scoreManager;
        private IHealthManager _healthManager;
        private PipesSelector _pipesSelector;
        private SpawnItems _spawnItems;

        private AudioInitiator _audioInitiator; 

        private IEnumerator _spawnCircles;

        private void OnPlayerLose()
        {
            _audioInitiator.PlayGameOver();
            
            EndGame();

            GameState.SwitchTo(GameState.State.GameOver);
        }

        private void OnReachHealPoint()
        {
            _healthManager.IncreaseHealth();
        }

        private IEnumerator SpawnCircles()
        {
            while (GameState.CurrentState != GameState.State.GameOver)
            {
                _spawnItems.Spawn();

                yield return new WaitForSeconds(Random.Range(1f, 1.5f));
            }
        }

        private void OnCircleTouchPipe(bool isColorsEquals)
        {
            if (isColorsEquals)
            {
                _scoreManager.IncreaseScore();
                _audioInitiator.PlayCorrect();
            }
            else
            {
                _healthManager.DecreaseHealth();
                _audioInitiator.PlayIncorrect();
            }
        }
        
        public Game(IScoreManager scoreManager, IHealthManager healthManager, AudioInitiator audioInitiator)
        {
            _scoreManager = scoreManager;
            _healthManager = healthManager;
            _audioInitiator = audioInitiator;
            
            _pipesSelector = DI.Get<PipesSelector>();
            _spawnItems = DI.Get<SpawnItems>();

            _spawnCircles = SpawnCircles();
            _spawnItems.StartCoroutine(_spawnCircles);
            
            _pipesSelector.CircleTouchPipe += OnCircleTouchPipe;
            _scoreManager.HealPoint += OnReachHealPoint;
            _healthManager.NoHealth += OnPlayerLose;
        }

        public void EndGame()
        {
            _spawnItems.StopCoroutine(_spawnCircles);
            _spawnItems.HideItems();
            _pipesSelector.AllPipesToDefaultPosition();
            
            _pipesSelector.CircleTouchPipe -= OnCircleTouchPipe;
            _scoreManager.HealPoint -= OnReachHealPoint;
            _healthManager.NoHealth -= OnPlayerLose;
        }
    }
}