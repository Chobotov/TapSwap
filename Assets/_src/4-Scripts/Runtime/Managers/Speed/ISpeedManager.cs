namespace TapSwap.Managers.Speed
{
    public interface ISpeedManager
    {
        float CurrentSpeed { get; }

        void Reset();
    }
}