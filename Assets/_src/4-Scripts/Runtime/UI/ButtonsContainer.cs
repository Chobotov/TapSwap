using UnityEngine;
using UnityEngine.UI;

namespace TapSwap.UI
{
    public class ButtonsContainer : MonoBehaviour
    {
        [field:SerializeField] public Toggle Audio { get; private set; }
        [field:SerializeField] public Button Resume { get; private set; }
        [field:SerializeField] public Button Restart { get; private set; }
        [field:SerializeField] public Button Exit { get; private set; }

        private void Awake()
        {
            HideButtons();
        }

        public void ShowButtons()
        {
            gameObject.SetActive(true);
        }

        public void HideButtons()
        {
            gameObject.SetActive(false);
        }
    }
}