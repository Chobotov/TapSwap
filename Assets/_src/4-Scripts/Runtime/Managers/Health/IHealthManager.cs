using System;

namespace TapSwap.Managers.Health
{
    public interface IHealthManager
    {
        Action HealthIncrease { get; set; }
        Action HealthDecrease { get; set; }
        
        int CurrentHealth { get; }
        
        void IncreaseHealth();
        void DecreaseHealth();
        void Reset();
    }
}