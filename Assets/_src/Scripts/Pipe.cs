using TapSwap.Scripts.Managers;
using UnityEngine;

namespace TapSwap
{
    public class Pipe : MonoBehaviour
    {
        private const int Multiple = 5;
        private const int MinHeartNum = 2;

        [SerializeField] private GameManager _gameManager;
        [SerializeField] AudioClip _check, _fail;

        private AudioSource _audio;
        private ScoreManager _scoreManager;

        private bool CanIncreaseHealth => _scoreManager.CurrentScore % Multiple == 0 && _gameManager.HealthManager.CurrentHealth < MinHeartNum;

        private void Start()
        {
            _scoreManager = _gameManager.GetComponent<ScoreManager>();
            _audio = Camera.main.GetComponent<AudioSource>();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals(gameObject.tag))
            {
                _scoreManager.IncreaseValue();
                
                if (CanIncreaseHealth) _gameManager.HealthManager.IncreaseHealth();

                _audio.PlayOneShot(_check);
            }
            else
            {
                _scoreManager.DecreaseValue();
                _gameManager.HealthManager.DecreaseHealth();
                _audio.PlayOneShot(_fail);
            }
            
            _scoreManager.SpeedCheck();
            _gameManager.UpdateRecord(_scoreManager.CurrentScore);
            
            collision.gameObject.SetActive(false);
        }
    }
}
