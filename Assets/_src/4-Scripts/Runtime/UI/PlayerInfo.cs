using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TapSwap.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private Text _currentScore;
        [SerializeField] private TextMeshProUGUI _recordScore;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public Text CurrentScore => _currentScore;
        public TextMeshProUGUI RecordScore => _recordScore;

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