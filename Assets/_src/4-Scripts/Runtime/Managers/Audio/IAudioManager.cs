namespace TapSwap.Managers.Audio
{
    public interface IAudioManager
    {
        bool IsAudioEnable { get; }
        void SetAudioState(bool state);
    }
}