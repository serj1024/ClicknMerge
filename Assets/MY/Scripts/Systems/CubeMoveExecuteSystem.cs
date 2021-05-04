using DG.Tweening;
using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.Entity.Events;

namespace MY.Scripts.Systems
{
    internal class CubeMoveExecuteSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CubeViewRef, OnMoveEvent> _filter = null;
        private readonly Configuration _configuration = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var cubeView = ref _filter.Get1(i).Value;
                ref var targetPosition = ref _filter.Get2(i).TargetPosition.value;
                _filter.GetEntity(i).Get<Position>().value = targetPosition;
                
                cubeView.transform.DOMove(targetPosition, _configuration.CubeMoveDuration);
            }
        }
    }
}