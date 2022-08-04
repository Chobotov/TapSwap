using TapSwap.Managers.Score;

namespace TapSwap.Managers.Speed
{
    public class SpeedManager : ISpeedManager
    {
        private IScoreManager _scoreManager;
        
        private float _currentSpeed;

        private void OnScoreIncreased()
        {
            if (_scoreManager.CurrentScore % 5 != 0) return;

            _currentSpeed++;
        }
        private void OnScoreDecreased()
        {
            if (_scoreManager.CurrentScore % 5 != 0) return;

            _currentSpeed--;
        }
        
        public SpeedManager(IScoreManager scoreManager)
        {
            _scoreManager = scoreManager;
            _currentSpeed = 3;

            _scoreManager.ScoreIncrease += OnScoreIncreased;
            _scoreManager.ScoreDecrease += OnScoreDecreased;
        }

        public float CurrentSpeed => _currentSpeed;
    }
}