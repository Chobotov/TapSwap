using System;
using UnityEngine;

namespace TapSwap.Scripts.Managers
{
    public class HealthManager : MonoBehaviour, IHealthManager
    {
        private const int DefaultPlayerHealth = 2;
        
        [SerializeField] private GameObject[] _hearts;

        private int _currentHealth;
        
        private void UpdateHealthView()
        {
            for (var i = 0; i <= _hearts.Length; i++) _hearts[i].SetActive(false);
            for (var i = 0; i <= _currentHealth; i++) _hearts[i].SetActive(true);
        }
        
        private void Start()
        {
            _currentHealth = DefaultPlayerHealth;
        }

        public Action HealthIncrease { get; set; }
        public Action HealthDecrease { get; set; }
        
        public int CurrentHealth => _currentHealth;
        
        public void IncreaseHealth()
        {
            _currentHealth++;
            UpdateHealthView();
            HealthIncrease?.Invoke();
        }

        public void DecreaseHealth()
        {
            _currentHealth--;
            HealthDecrease?.Invoke();
        }
    }
}