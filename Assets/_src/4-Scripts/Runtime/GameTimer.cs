using System;
using System.Collections;
using TapSwap.Runtime.App;
using UnityEngine;
using UnityEngine.UI;

namespace TapSwap
{
    public class GameTimer : MonoBehaviour
    {
        private const int TimerDuration = 3;
        
        [SerializeField] private Text _timer;

        private void Start()
        {
            DI.Add(this);
        }

        private IEnumerator Timer(Action onTimerEnd)
        {
            _timer.gameObject.SetActive(true);

            for (var i = TimerDuration; i >= 0; i--)
            {
                _timer.text = $"{i}";

                yield return new WaitForSecondsRealtime(1);
            }

            _timer.gameObject.SetActive(false);
            _timer.text = "";

            Time.timeScale = 1f;
            
            onTimerEnd?.Invoke();
        }

        public void StartTimer(Action onTimerEnd)
        {
            StartCoroutine(nameof(Timer), onTimerEnd);
        }
    }
}