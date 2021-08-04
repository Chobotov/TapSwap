using UnityEngine;
using UnityEngine.UI;

namespace TapSwap
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private int _value;
        [SerializeField] private float _speed;
        [SerializeField] private Text _scoreTxt;
        [SerializeField] private Rigidbody2D _redDropItem;
        [SerializeField] private Rigidbody2D _blueDropItem;
        [SerializeField] private Rigidbody2D _greenDropItem;

        public int Value => _value;
        public float Speed => _speed;

        public void IncreaseValue(int value = 1) => _value += value;
        public void DecreaseValue(int value = 1) => _value -= value;
        public void ClearScore() => _value = 0;
        
        private void Start()
        {
            _value = 0;
            _speed = 5f;
            _scoreTxt.text = $"{_value}";
        }
        
        public void SpeedCheck()
        {
            _scoreTxt.text = $"{_value}";
            
            UpdateSpeed(5f, 4f, 0.3f);
            UpdateSpeed(10f, 3f, 0.4f);
            UpdateSpeed(15f, 2f, 0.5f);
            UpdateSpeed(20f, 1f, 0.6f);
            UpdateSpeed(25f, 1f, 0.7f);
        }

        private void UpdateSpeed(float valueBorder, float speed, float gravity)
        {
            if (_value >= valueBorder)
            {
                _speed = speed;
                ChangeGravity(gravity);
            }
            else if (_value < valueBorder && _value >= valueBorder - 5f)
            {
                _speed = speed + 1f;
                ChangeGravity(gravity - .1f);
            }
        }

        private void ChangeGravity(float value)
        {
            _redDropItem.GetComponent<Rigidbody2D>().gravityScale = value;
            _blueDropItem.GetComponent<Rigidbody2D>().gravityScale = value;
            _greenDropItem.GetComponent<Rigidbody2D>().gravityScale = value;
        }
    }
}
