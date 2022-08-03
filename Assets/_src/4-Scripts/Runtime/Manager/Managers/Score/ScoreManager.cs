using System;

namespace TapSwap.Managers.Score
{
    public class ScoreManager : IScoreManager
    {
        private int _score;
        private int _recordScore;
        private float _speed;
        
        public ScoreManager()
        {
            
        }

        public Action<int> ScoreChanged { get; set; }
        
        public int CurrentScore => _score;
        public int RecordScore => _recordScore;
        public float Speed => _speed;

        public void ClearScore()
        {
            _score = 0;   
            ScoreChanged?.Invoke(_score);
        }

        public void IncreaseValue(int value = 1)
        {
            _score += value;

            if (_score > _recordScore) _recordScore = _score;
            
            ScoreChanged?.Invoke(_score);
        }

        public void DecreaseValue(int value = 1)
        {
            _score -= value;
            
            ScoreChanged?.Invoke(_score);
        }
    }
}
