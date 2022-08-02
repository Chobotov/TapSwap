using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TapSwap
{
    public class GameTimer : MonoBehaviour
    {
        private const int TimerDuration = 3;
        
        [SerializeField] private Text _timer;
        
        private IEnumerator Timer(Action onTimerEnd)
        {
            _timer.gameObject.SetActive(true);

            for (var i = TimerDuration; i >= 0; i--)
            {
                _timer.text = $"{i}";

                yield return new WaitForSeconds(1);
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