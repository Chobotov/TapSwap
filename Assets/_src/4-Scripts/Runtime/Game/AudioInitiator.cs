using TapSwap.Managers.Audio.Data;
using TapSwap.Runtime.App;
using UnityEngine;

namespace TapSwap.Game
{
    public class AudioInitiator : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [Space]
        [SerializeField] private AudioConfig _audioConfig;
        
        private void Awake()
        {
            DI.Add(this);
        }

        public void SetAudioState(bool isOn)
        {
            _source.mute = !isOn;
        }

        public void PlayCorrect()
        {
            _source.PlayOneShot(_audioConfig.CorrectColors);
        }

        public void PlayIncorrect()
        {
            _source.PlayOneShot(_audioConfig.InCorrectColors);
        }

        public void PlayGameOver()
        {
            _source.PlayOneShot(_audioConfig.GameOver);
        }
    }
}