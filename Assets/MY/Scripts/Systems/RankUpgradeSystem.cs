using System.Linq;
using Leopotam.Ecs;
using MY.Scripts.UnityComponents;
using UnityEngine;

namespace MY.Scripts.Systems
{
    internal class RankUpgradeSystem : IEcsInitSystem
    {
        private readonly PlayerData _playerData = null;
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;
        private readonly SceneData _sceneData = null;

        public void Init()
        {
            _runtimeData.GameState.OnGameStateChange += OnGameStateChange;
            OnGameStateChange(_runtimeData.GameState.State);
        }

        private void OnGameStateChange(State state)
        {
            if (state != State.GameOver)
                return;

            var currentRank = _playerData.Rank;
            _playerData.Scors += _runtimeData.PlayerScore;
            Debug.Log("current scores: " + _playerData.Scors);
            var nextRank = _configuration.ScorsByRank.Last(pair => pair.Value <= _playerData.Scors).Key;
            Debug.Log("currentRank: " + currentRank + " nextRank: " + nextRank);

            if (nextRank > currentRank)
            {
                _playerData.Rank = nextRank;
                
                _sceneData.UI.GameOverScreen.NewRankPanel.SetActive(true);
                
                var rankIcon = _configuration.IconsByRank[_playerData.Rank];
                _sceneData.UI.GameOverScreen.RankIcon.sprite = rankIcon;
                
                _sceneData.UI.GameOverScreen.RankUpTMP.SetText("from " + currentRank + " to " + nextRank);
            }
            else
            {
                _sceneData.UI.GameOverScreen.NewRankPanel.SetActive(false);
            }
        }
    }
}