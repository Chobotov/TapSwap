using UnityEngine;

namespace TapSwap
{
    public class ControlPipes : MonoBehaviour
    {
        [SerializeField] private GameObject RedPipe, GreenPipe, BluePipe;
        [SerializeField] private float _redX = -0.9349999f;
        [SerializeField] private float _greenX = 0.85f;
        [SerializeField] private float _blueX = 0f;

        private Camera _camera;
        
        private GameObject _pipe;
        private bool _isDown = true;
        private bool _click;
        private Vector3 _coord;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void PipeUpDown()
        {
            var hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.transform == null) return;
            
            if (_isDown)
            {
                if (hit.transform.gameObject.TryGetComponent(out Pipe pipe))
                {
                    _isDown = false;
                    _pipe = hit.transform.gameObject;
                    _coord = _pipe.transform.position;
                    _pipe.transform.position += new Vector3(0f, 0.2f, 0f);
                }
            }
            else
            {
                if (hit.transform.gameObject.TryGetComponent(out Pipe pipe))
                {
                    var nextPipe = hit.transform.gameObject;
                    var nextCor = nextPipe.transform.position;
                    
                    if (_pipe != nextPipe)
                    {
                        _pipe.transform.position = nextCor;
                        nextPipe.transform.position = _coord;
                        _isDown = true;
                    }

                    if (_pipe == nextPipe && _click)
                    {
                        _pipe.transform.position = _coord;
                        _isDown = true;
                    }
                }
            }
        }

        public void SelectPipe()
        {
            if (Input.GetMouseButtonUp(0) && !_click) _click = true;

            else if (Input.GetMouseButtonDown(0)) PipeUpDown();
        }
    }
}
