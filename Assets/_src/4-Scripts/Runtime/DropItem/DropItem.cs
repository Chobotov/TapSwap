using UnityEngine;

namespace TapSwap.DropItem
{
    [ExecuteInEditMode]
    public class DropItem : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private bool _isPaused;
        private bool CanFalling => gameObject.activeSelf && !_isPaused;
        
        private float _speed;

        public Color Color => _spriteRenderer.color;
        
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Tick()
        {
            if (!CanFalling) return;
            
            transform.position += Vector3.down * Time.deltaTime * _speed;
        }

        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        public DropItem SetPosition(Vector3 pos)
        {
            transform.position = pos;

            return this;
        }
        
        public DropItem SetColor(Color color)
        {
            _spriteRenderer.color = color;

            return this;
        }

        public DropItem SetSpeed(float speed)
        {
            _speed = speed;

            return this;
        }
    }
}