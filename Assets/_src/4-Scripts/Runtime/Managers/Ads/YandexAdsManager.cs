using TapSwap.Managers.Health;
using TapSwap.Managers.Score;

namespace TapSwap.Managers.Ads
{
    public class YandexAdsManager
    {
        private const int AdTrigger = 3;
        
        private IScoreManager _scoreManager;
        private IHealthManager _healthManager;
        
        private YandexSDK _yandexSDK;

        private int _loseCounter;

        public YandexAdsManager(IScoreManager scoreManager, IHealthManager healthManager)
        {
            _yandexSDK = YandexSDK.instance;

            _scoreManager = scoreManager;
            _healthManager = healthManager;

            _healthManager.NoHealth += OnPlayerLose;
        }

        private void OnPlayerLose()
        {
            _loseCounter++;

            if (_loseCounter >= AdTrigger)
            {
                _loseCounter = 0;
                _yandexSDK.ShowInterstitial();
            }
        }
    }
}