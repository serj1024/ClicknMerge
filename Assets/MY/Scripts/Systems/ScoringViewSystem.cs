using Leopotam.Ecs;
using MY.Scripts.UnityComponents;

namespace MY.Scripts.Systems
{
    internal class ScoringViewSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly RuntimeData _runtimeData = null;
        private readonly SceneData _sceneData = null;

        public void Init()
        {
            if(_runtimeData.GameState.State == State.Game)
                PrintPlayerScore();
            _runtimeData.GameState.OnGameStateChange += OnGameStateChange;
            _runtimeData.OnChangeScore += PrintPlayerScore;
        }

        private void OnGameStateChange(State state)
        {
            switch (state)
            {
                case State.Game:
                    PrintPlayerScore();
                    break;
                case State.GameOver:
                    _sceneData.UI.GameOverScreen.ScoreTMP.SetText(_runtimeData.PlayerScore.ToString());
                    break;
            }
        }

        private void PrintPlayerScore()
        {
            switch (_runtimeData.GameMode)
            {
                case GameMode.FakeMultiplayer:
                    _sceneData.UI.GameScreen.PlayerScoreTextMultiplayerMode.SetText(_runtimeData.PlayerScore
                        .ToString());
                    _sceneData.UI.GameScreen.BotScoreText.SetText(_runtimeData.BotScore.ToString());
                    break;
                case GameMode.Endless:
                    _sceneData.UI.GameScreen.PlayerScoreTextEndlessMode.SetText(_runtimeData.PlayerScore.ToString());
                    break;
            }
        }

        public void Destroy()
        {
            _runtimeData.GameState.OnGameStateChange -= OnGameStateChange;
            _runtimeData.OnChangeScore -= PrintPlayerScore;
        }
    }
}