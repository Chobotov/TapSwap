using System;

namespace TapSwap.Scripts.Managers
{
    public interface IScoreManager
    {
        Action ScoreChanged { get; set; }
        int CurrentScore { get; }
    }
}