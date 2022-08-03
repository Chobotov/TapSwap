using UnityEngine;

namespace TapSwap.UI
{
    public abstract class Screen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public abstract ScreenType Type { get; }

        public void Show()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}