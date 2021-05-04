using System;
using GoogleMobileAds.Api;
using Leopotam.Ecs;
using MY.Scripts.Entity.Events;
using UnityEngine.SceneManagement;

namespace MY.Scripts.Systems
{
    internal class ShowRewardedAdSystem : IEcsInitSystem, IEcsRunSystem
    {
        private RewardedAd _rewardedAd;
        private String _adId = "ca-app-pub-7462652161313543/6402744673"; //release
        //private String _adId = "ca-app-pub-3940256099942544/5224354917"; //test
        private readonly EcsFilter<OnShowRewardedAdEvent> _filter;
        private readonly PlayerData _playerData;
        private readonly RuntimeData _runtimeData;
        private readonly Configuration _configuration;

        public void Init()
        {
            MobileAds.Initialize(initStatus =>
            {
                _rewardedAd = new RewardedAd(_adId);
                _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
                _rewardedAd.OnAdFailedToLoad += HandleRewardedAdError;
                _rewardedAd.OnAdFailedToShow += HandleRewardedAdError;
                _rewardedAd.OnAdClosed += HandleRewardedAdError;

                // Create an empty ad request.
                AdRequest request = new AdRequest.Builder().Build();
                // Load the rewarded ad with the request.
                _rewardedAd.LoadAd(request);
            });
        }

        private void HandleRewardedAdError(object sender, EventArgs eventArgs)
        {
            _playerData.Cash += _runtimeData.CalculatedCash;
            SceneManager.LoadScene(0);
        }

        private void HandleUserEarnedReward(object sender, Reward e)
        {
            _playerData.Cash += _runtimeData.CalculatedCash * _configuration.AdCashCoefficient;
            SceneManager.LoadScene(0);
        }

        public void Run()
        {
            if (_filter.IsEmpty())
                return;
            
            if (_rewardedAd.IsLoaded()) {
                _rewardedAd.Show();
            }
        }
    }
}