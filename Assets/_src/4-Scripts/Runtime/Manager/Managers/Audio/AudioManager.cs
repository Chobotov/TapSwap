using UnityEngine;

namespace TapSwap.Managers.Audio
{
    public class AudioManager : IAudioManager
    {
        private const string SoundSaveKey = "Sound";

        private bool _isSoundEnable;

        private void LoadAudioState()
        {
            _isSoundEnable = PlayerPrefs.GetInt(SoundSaveKey) > 0;
        }

        private void Save()
        {
            PlayerPrefs.SetInt(SoundSaveKey, _isSoundEnable ? 1 : 0);
        }
        
        public AudioManager()
        {
            LoadAudioState();
        }

        public void SetAudioState(bool state)
        {
            _isSoundEnable = true;

            Save();
        }
    }
}