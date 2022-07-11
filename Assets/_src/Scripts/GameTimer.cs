using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TapSwap
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private Text _timer;
        
        private IEnumerator Timer(Action onTimerEnd)
        {
            var num = 3;
            _timer.gameObject.SetActive(true);
            
            for (var i = 0; i < 3; i++)
            {
                _timer.text = $"{num}";
                num -= 1;
                
                yield return new WaitForSecondsRealtime(1);
            }

            _timer.gameObject.SetActive(false);

            Time.timeScale = 1f;
            
            onTimerEnd?.Invoke();
        }

        public void StartTimer(Action onTimerEnd)
        {
            StartCoroutine(nameof(Timer), onTimerEnd);
        }
    }
}