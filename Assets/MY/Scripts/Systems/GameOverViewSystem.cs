using System;
using Leopotam.Ecs;
using MY.Scripts.UnityComponents;

namespace MY.Scripts.Systems
{
    internal class GameOverViewSystem : IEcsInitSystem
    {
        private readonly PlayerData _playerData = null;
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

            switch (_runtimeData.GameMode)
            {
                case GameMode.Endless:
                    SetNewRecordText();
                    break;
                case GameMode.FakeMultiplayer:
                    SetMultiplayerInfoText();
                    break;
            }
        }

        private void SetMultiplayerInfoText()
        {
            if (_runtimeData.PlayerScore > _runtimeData.BotScore)
            {
                _sceneData.UI.GameOverScreen.TitleTMP.SetText("You win!");
            }
            else if (_runtimeData.PlayerScore < _runtimeData.BotScore)
            {
                _sceneData.UI.GameOverScreen.TitleTMP.SetText("You lose!");
            }
            else if (_runtimeData.PlayerScore == _runtimeData.BotScore)
            {
                _sceneData.UI.GameOverScreen.TitleTMP.SetText("Drawn!");
            }
        }

        private void SetNewRecordText()
        {
            var newRecord = _runtimeData.PlayerScore;
            var oldRecord = _playerData.EndlessRecord;

            if (newRecord > oldRecord)
            {
                _playerData.EndlessRecord = newRecord;
                _sceneData.UI.GameOverScreen.TitleTMP.SetText("New Record!");
            }
            else
            {
                _sceneData.UI.GameOverScreen.TitleTMP.SetText("Game Over!");
            }
        }
    }
}