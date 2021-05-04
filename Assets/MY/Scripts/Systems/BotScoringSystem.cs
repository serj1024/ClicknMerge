using System;
using System.Threading.Tasks;
using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.Entity.Events;
using MY.Scripts.Systems;
using Random = UnityEngine.Random;

namespace MY.Scripts
{
    internal class BotScoringSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<OnCubeMoveTimedEvent> _onCubeMove = null;
        private readonly EcsFilter<CubeViewRef, Score> _cubeScores;
        private readonly EcsFilter<OnMergeCompleteEvent, Score> _mergeCube = null;
        private readonly Configuration _configuration = null;

        private readonly RuntimeData _runtimeData = null;

        public void Init()
        {
        }

        public void Run()
        {
            if (_runtimeData.GameState.State != State.Game && _runtimeData.GameMode != GameMode.FakeMultiplayer)
                return;

            if (!_onCubeMove.IsEmpty())
            {
                AddBotScoreAsync(_cubeScores.Get2(Random.Range(0, _cubeScores.GetEntitiesCount() - 1)).Value);
            }

            if (!_mergeCube.IsEmpty())
            {
                if (Random.Range(-1f, _configuration.BotDifficulty) < 0)
                    AddBotScoreAsync(_mergeCube.Get2(0).Value);
            }
        }

        private async void AddBotScoreAsync(int score)
        {
            var waitingTime = Random.Range(0f, _configuration.MaxDelayAddingBotScore);
            await Task.Delay(TimeSpan.FromSeconds(waitingTime));
            _runtimeData.BotScore += score;
        }
    }
}