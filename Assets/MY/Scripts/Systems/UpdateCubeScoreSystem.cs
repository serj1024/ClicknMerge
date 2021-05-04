using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.Entity.Events;

namespace MY.Scripts.Systems
{
    internal class UpdateCubeScoreSystem : IEcsRunSystem
    {
        private readonly EcsFilter<OnMergeCompleteEvent, Score> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                _filter.Get2(i).Value += _filter.Get2(i).Value;
            }
        }
    }
}