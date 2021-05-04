using System;
using Leopotam.Ecs;
using MY.Scripts.Entity.Events;
using MY.Scripts.Extensions;
using MY.Scripts.UnityComponents;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MY.Scripts.Systems
{
    internal class CalculateCashSystem : IEcsInitSystem
    {
        private readonly RuntimeData _runtimeData = null;
        private readonly PlayerData _playerData = null;
        private readonly SceneData _sceneData = null;
        private readonly Configuration _configuration = null;
        private readonly EcsWorld _world = null;

        private int _calculatedCash;
        
        public void Init()
        {
            _runtimeData.GameState.OnGameStateChange += OnGameStateChange;
            _sceneData.UI.GameOverScreen.BackToMenuButton.onClick.AddListener(OnBackToMenu);
            _sceneData.UI.GameOverScreen.ShowRewardedAdButton.onClick.AddListener(OnRewardedAd);
        }

        private void OnRewardedAd()
        {
            Debug.Log("Show AD");
            _world.SendMessage(new OnShowRewardedAdEvent());
        }

        private void OnBackToMenu()
        {
            _playerData.Cash += _calculatedCash;
            SceneManager.LoadScene(0);
        }

        private void OnGameStateChange(State state)
        {
            if (state == State.GameOver)
            {
                _calculatedCash = CalculateCash();
                _runtimeData.CalculatedCash = _calculatedCash;
                _sceneData.UI.GameOverScreen.CashTMP.SetText("+ " + _calculatedCash);
            }
        }
        
        private int CalculateCash()
        {
            return Convert.ToInt32(_runtimeData.PlayerScore * _configuration.RatioOfCache);
        }
    }
}