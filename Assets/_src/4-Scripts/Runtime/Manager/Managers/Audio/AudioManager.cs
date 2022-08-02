using UnityEngine;

namespace TapSwap.Scripts.Managers
{
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        private const string SoundSaveKey = "Sound";
        
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _gameOver;
        [SerializeField] private GameObject _lines;

        private bool _isSoundEnable;

        private void UpdateData()
        {
            _lines.gameObject.SetActive(_isSoundEnable);
            PlayerPrefs.SetInt(SoundSaveKey, _isSoundEnable ? 1 : 0);
        }
        
        private void Start()
        {
            _isSoundEnable = PlayerPrefs.GetInt(SoundSaveKey) > 0;
        }

        public void EnableSound()
        {
            _isSoundEnable = true;
            _audioSource.volume = 1f;
            
            UpdateData();
        }

        public void DisableSound()
        {
            _isSoundEnable = false;
            _audioSource.volume = 0f;
            
            UpdateData();
        }

        public void PlayGameOverSound()
        {
            _audioSource.PlayOneShot(_gameOver);
        }
    }
}