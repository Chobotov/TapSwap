using TapSwap.Managers.Score;

namespace TapSwap.Managers.Speed
{
    public class SpeedManager : ISpeedManager
    {
        private const int StartItemsSpeed = 4;
        private const int ChangeSpeedTrigger = 3;
        private const float SpeedDeltaValue = 0.7f;

        private IScoreManager _scoreManager;
        
        private bool CanChangeSpeed => _scoreManager.CurrentScore % ChangeSpeedTrigger == 0;

        private float _currentSpeed;

        private void OnScoreIncreased()
        {
            if (!CanChangeSpeed) return;

            _currentSpeed += SpeedDeltaValue;
        }
        private void OnScoreDecreased()
        {
            if (!CanChangeSpeed) return;

            _currentSpeed -= SpeedDeltaValue;

            if (_currentSpeed < StartItemsSpeed) _currentSpeed = StartItemsSpeed;
        }
        
        public SpeedManager(IScoreManager scoreManager)
        {
            _scoreManager = scoreManager;
            _currentSpeed = StartItemsSpeed;

            _scoreManager.ScoreIncrease += OnScoreIncreased;
            _scoreManager.ScoreDecrease += OnScoreDecreased;
        }

        public float CurrentSpeed => _currentSpeed;
        public void Reset()
        {
            _currentSpeed = StartItemsSpeed;
        }
    }
}