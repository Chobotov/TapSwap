using System;

namespace TapSwap.Scripts.Managers
{
    public interface IHealthManager
    {
        Action HealthIncrease { get; set; }
        Action HealthDecrease { get; set; }
        
        int CurrentHealth { get; }
        
        void IncreaseHealth();
        void DecreaseHealth();
    }
}