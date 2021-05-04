using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.Entity.Events;

namespace MY.Scripts.Systems
{
    internal class PlayerScoringSystem : IEcsRunSystem
    {
        private readonly RuntimeData _runtimeData = null;
        private readonly EcsFilter<OnMergeCompleteEvent, Score> _filter = null;

        public void Run()
        {
            if (_runtimeData.GameState.State != State.Game && _runtimeData.GameState.State != State.Train)
                return;
            
            foreach (var i in _filter)
            {
                _runtimeData.PlayerScore += _filter.Get2(i).Value;
            }
        }
    }
}