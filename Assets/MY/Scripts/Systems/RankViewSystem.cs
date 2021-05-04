using Leopotam.Ecs;
using MY.Scripts.UnityComponents;
using UnityEngine;

namespace MY.Scripts.Systems
{
    internal class RankViewSystem : IEcsInitSystem
    {
        private readonly RuntimeData _runtimeData = null;
        private readonly SceneData _sceneData = null;
        private readonly PlayerData _playerData = null;
        private readonly Configuration _configuration = null;
        
        public void Init()
        {
            _runtimeData.GameState.OnGameStateChange += OnGameStateChange;
            OnGameStateChange(_runtimeData.GameState.State);
        }

        private void OnGameStateChange(State state)
        {
            var playerRankText = _playerData.Rank + " Rank";
            var playerIcon = _configuration.IconsByRank[_playerData.Rank];
            
            switch (state)
            {
                case State.GameOver:
                    break;
                case State.Game:
                    switch (_runtimeData.GameMode)
                    {
                        case GameMode.FakeMultiplayer:
                            var randomBotRank = GetRandomBotRank();
                            var botRankText = randomBotRank + " Rank";
                            var botIcon = _configuration.IconsByRank[randomBotRank];
                            
                            _sceneData.UI.GameScreen.PlayerIconMultiplayerMode.sprite = playerIcon;
                            _sceneData.UI.GameScreen.BotIcon.sprite = botIcon;
                            _sceneData.UI.GameScreen.PlayerRank.SetText(playerRankText);
                            _sceneData.UI.GameScreen.BotRank.SetText(botRankText);
                            break;
                        
                        case GameMode.Endless:
                            _sceneData.UI.GameScreen.PlayerIconEndlessMode.sprite = playerIcon;
                            break;
                    }
                    break;
                case State.Menu:
                    _sceneData.UI.MenuScreen.PlayerIcon.sprite = playerIcon;
                    _sceneData.UI.MenuScreen.PlayerRank.SetText(playerRankText);
                    break;
            }
        }

        private int GetRandomBotRank()
        {
            var randomBotRank = _playerData.Rank + Random.Range(-2, 2);
            if (randomBotRank > _configuration.IconsByRank.Count)
                randomBotRank = _configuration.IconsByRank.Count;
            if (randomBotRank <= 0)
                randomBotRank = 1;
            return randomBotRank;
        }
    }
}