using UnityEngine;

namespace TapSwap
{
    public class Pipe : MonoBehaviour
    {
        private const int Multiple = 5;
        private const int MinHeartNum = 2;

        [SerializeField] AudioClip _check, _fail;
        
        private GameManager _gameManager;
        private AudioSource _audio;
        private Score _score;

        private bool canIncreaseHealth => _score.Value % Multiple == 0 && _gameManager.HeartNum < MinHeartNum;

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _score = _gameManager.GetComponent<Score>();
            _audio = Camera.main.GetComponent<AudioSource>();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals(gameObject.tag))
            {
                _score.IncreaseValue();
                
                if (canIncreaseHealth) _gameManager.IncreaseHeartNum();

                _audio.PlayOneShot(_check);
            }
            else
            {
                _score.DecreaseValue();
                _gameManager.DecreaseHeartNum();
                _audio.PlayOneShot(_fail);
            }
            
            _gameManager.ShowHealth(_gameManager.HeartNum, true);
            _score.SpeedCheck();
            _gameManager.UpdateRecord(_score.Value);
            
            Destroy(collision.gameObject);
        }
    }
}
