using UnityEngine;
using UnityEngine.UI;

namespace TapSwap.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ButtonsContainer : MonoBehaviour
    {
        [field:SerializeField] public Toggle Audio { get; private set; }
        [field:SerializeField] public Button Resume { get; private set; }
        [field:SerializeField] public Button Restart { get; private set; }
        [field:SerializeField] public Button Exit { get; private set; }

        private CanvasGroup _canvasGroup;
        
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            
            Hide();
        }

        public void Show()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}