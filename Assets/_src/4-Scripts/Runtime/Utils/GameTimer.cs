using System;
using System.Collections;
using TapSwap.Runtime.App;
using UnityEngine;
using UnityEngine.UI;

namespace TapSwap.Utils
{
    public class GameTimer : MonoBehaviour
    {
        private const int TimerDuration = 3;
        
        [SerializeField] private Text _time;

        private void Awake()
        {
            DI.Add(this);
        }

        private IEnumerator Timer(Action onTimerEnd)
        {
            _time.gameObject.SetActive(true);

            for (var i = TimerDuration; i >= 0; i--)
            {
                _time.text = $"{i}";

                yield return new WaitForSecondsRealtime(1);
            }

            _time.gameObject.SetActive(false);
            _time.text = "";

            Time.timeScale = 1f;
            
            onTimerEnd?.Invoke();
        }

        public void StartTimer(Action onTimerEnd)
        {
            StartCoroutine(nameof(Timer), onTimerEnd);
        }

        public void StopTimer()
        {
            StopAllCoroutines();
        }
    }
}