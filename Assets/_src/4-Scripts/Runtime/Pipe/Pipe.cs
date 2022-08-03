using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TapSwap.Pipe
{
    public class Pipe : MonoBehaviour, IPointerClickHandler
    {
        private const float YOffset = .3f;

        [SerializeField] private Color _color;

        public Action<Pipe> PipeClicked;
        public Action<Color, Color> CircleTouch;

        public void Up()
        {
            transform.position += Vector3.up * YOffset;
        }

        public void Down()
        {
            transform.position += Vector3.down * YOffset;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PipeClicked?.Invoke(this);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out DropItem.DropItem item))
            {
                CircleTouch?.Invoke(_color, item.Color);
                
                Debug.Log("Touched");
            }

            Debug.Log("Touch");
        }
    }
}