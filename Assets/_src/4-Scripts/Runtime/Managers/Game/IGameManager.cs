namespace TapSwap.Managers.Game
{
    public interface IGameManager
    {
        void StartSession();
        void Start();
        void Pause();
        void Resume();
        void Restart();
        void GameOver();
        void Exit();
    }
}