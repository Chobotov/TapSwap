using TapSwap.Managers.Health;

namespace TapSwap.Managers.Ads
{
    public class YandexAdsManager
    {
        private IHealthManager _healthManager;
        
        private YandexSDK _yandexSDK;

        public YandexAdsManager(IHealthManager healthManager)
        {
            _yandexSDK = YandexSDK.instance;
            _healthManager = healthManager;

            _healthManager.NoHealth += OnPlayerLose;
        }

        private void OnPlayerLose()
        {
            _yandexSDK.ShowInterstitial();
        }
    }
}