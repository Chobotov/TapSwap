using System;
using UnityEngine;

namespace TapSwap.Managers.Health
{
    public class HealthManager : IHealthManager
    {
        private const int DefaultPlayerHealth = 3;

        private int _currentHealth;

        public HealthManager()
        {
            Reset();
        }
        
        public Action HealthIncrease { get; set; }
        public Action HealthDecrease { get; set; }
        public Action NoHealth { get; set; }

        public int CurrentHealth => _currentHealth;
        
        public void IncreaseHealth()
        {
            _currentHealth++;

            if (_currentHealth >= DefaultPlayerHealth) _currentHealth = DefaultPlayerHealth;
            
            HealthIncrease?.Invoke();
        }

        public void DecreaseHealth()
        {
            _currentHealth--;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                NoHealth?.Invoke();
            }
            
            HealthDecrease?.Invoke();
        }

        public void Reset()
        {
            _currentHealth = DefaultPlayerHealth;
        }
    }
}