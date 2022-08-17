using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TapSwap.Pipe
{
    public class Pipe : MonoBehaviour, IPointerClickHandler
    {
        private const float YOffset = .3f;
        
        [SerializeField] private Color _color;

        private bool _isSelected;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out DropItem.DropItem item))
            {
                CircleTouch?.Invoke(_color, item.Color);
                item.Deactivate();
            }
        }

        public Action<Pipe> PipeSelected;
        public Action<Color, Color> CircleTouch;

        public void Up()
        {
            _isSelected = true;
            transform.position += Vector3.up * YOffset;
        }

        public void Down()
        {
            if (!_isSelected) return;

            _isSelected = false;
            transform.position += Vector3.down * YOffset;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PipeSelected?.Invoke(this);
        }

        #region WIP(v. 1.0.0.2) - механика смены труб через Drag and Drop

        /*
        
        private bool _isDragging;
        private Vector3 _currentPos;

        private void Awake()
        {
            _currentPos = transform.position;
        }
         
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Spot") && _isSelected && !_isDragging)
            {
                var otherPos = other.transform.position;
                transform.position = otherPos;
                other.transform.position = _currentPos;
                _currentPos = otherPos;
                _isSelected = false;
            }
        }*/

        /*public void OnDrag(PointerEventData eventData)
        {
            var eventDataPos = eventData.pointerCurrentRaycast.worldPosition;
            var newPos = new Vector3(eventDataPos.x, transform.position.y, 0f);
            
            transform.position = newPos;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isSelected = true;
            _isDragging = true;

            Debug.Log($"{_isSelected}");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
            
            Debug.Log($"{_isSelected}");
        }*/

        #endregion 
    }
}