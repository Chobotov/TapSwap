using System;

namespace TapSwap.Managers.Score
{
    public interface IScoreManager
    {
        Action<int> ScoreChanged { get; set; }
        int CurrentScore { get; }
        int RecordScore { get; }
    }
}