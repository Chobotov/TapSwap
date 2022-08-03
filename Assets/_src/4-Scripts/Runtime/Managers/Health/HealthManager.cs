using System;

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
        
        public int CurrentHealth => _currentHealth;
        
        public void IncreaseHealth()
        {
            _currentHealth++;
            HealthIncrease?.Invoke();
        }

        public void DecreaseHealth()
        {
            _currentHealth--;
            HealthDecrease?.Invoke();
        }

        public void Reset()
        {
            _currentHealth = DefaultPlayerHealth;
        }
    }
}