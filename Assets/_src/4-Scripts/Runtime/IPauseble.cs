namespace TapSwap
{
    public interface IPauseble
    {
        bool IsPaused { get; }
        void OnGameStateChanged(GameState.State state);
    }
}