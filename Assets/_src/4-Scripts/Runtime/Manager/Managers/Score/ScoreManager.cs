using System;
using UnityEngine;
using UnityEngine.UI;

namespace TapSwap.Scripts.Managers
{
    public class ScoreManager : MonoBehaviour, IScoreManager
    {
        [SerializeField] private int _score;
        [SerializeField] private float _speed;
        
        [SerializeField] private Text _scoreTxt;
        
        [SerializeField] private Rigidbody2D _redDropItem;
        [SerializeField] private Rigidbody2D _blueDropItem;
        [SerializeField] private Rigidbody2D _greenDropItem;

        private void Start()
        {
            _score = 0;
            _speed = 5f;
            _scoreTxt.text = $"{_score}";
        }
        
        public void SpeedCheck()
        {
            _scoreTxt.text = $"{_score}";
            
            UpdateSpeed(5f, 4f, 0.3f);
            UpdateSpeed(10f, 3f, 0.4f);
            UpdateSpeed(15f, 2f, 0.5f);
            UpdateSpeed(20f, 1f, 0.6f);
            UpdateSpeed(25f, 1f, 0.7f);
        }

        private void UpdateSpeed(float valueBorder, float speed, float gravity)
        {
            if (_score >= valueBorder)
            {
                _speed = speed;
                ChangeGravity(gravity);
            }
            else if (_score < valueBorder && _score >= valueBorder - 5f)
            {
                _speed = speed + 1f;
                ChangeGravity(gravity - .1f);
            }
        }

        private void ChangeGravity(float value)
        {
            _redDropItem.gravityScale = value;
            _blueDropItem.gravityScale = value;
            _greenDropItem.gravityScale = value;
        }

        public Action ScoreChanged { get; set; }
        public int CurrentScore => _score;
        public float Speed => _speed;

        public void ClearScore()
        {
            _score = 0;   
        }

        public void IncreaseValue(int value = 1)
        {
            _score += value;   
        }

        public void DecreaseValue(int value = 1)
        {
            _score -= value;
        }
    }
}
