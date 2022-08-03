using System.Collections;
using TapSwap.DropItem;
using TapSwap.Managers.Health;
using TapSwap.Managers.Score;
using TapSwap.Pipe;
using TapSwap.Runtime.App;
using UnityEngine;

namespace TapSwap
{
    public class GameSession
    {
        private IScoreManager _scoreManager;
        private IHealthManager _healthManager;
        private PipesSelector _pipesSelector;
        private SpawnItems _spawnItems;

        public GameSession()
        {
            _scoreManager = DI.Get<IScoreManager>();
            _healthManager = DI.Get<IHealthManager>();
            _pipesSelector = DI.Get<PipesSelector>();
            _spawnItems = DI.Get<SpawnItems>();

            _spawnItems.StartCoroutine(SpawnCircles());
            
            _pipesSelector.CircleTouchPipe += OnCircleTouchPipe;
        }
        
        private IEnumerator SpawnCircles()
        {
            while (GameState.CurrentState != GameState.State.GameOver)
            {
                _spawnItems.Spawn();

                yield return new WaitForSeconds(Random.Range(2, 5f));
            }
        }

        private void OnCircleTouchPipe(bool isColorsEquals)
        {
            if (isColorsEquals) _scoreManager.IncreaseScore();
            else
            {
                _scoreManager.DecreaseScore();
                _healthManager.DecreaseHealth();
            }
        }
    }
}