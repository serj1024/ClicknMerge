using DG.Tweening;
using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.Entity.Events;
using MY.Scripts.UnityComponents;

namespace MY.Scripts.Systems
{
    internal class MergeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<OnMergeEvent, Score, CubeViewRef, Position> _mergeEntities = null;
        
        private readonly Configuration _configuration = null;
        private readonly SceneData _sceneData = null;

        public void Run()
        {
            if(_mergeEntities.IsEmpty())
                return;
            
            MergeAll();
        }

        private void MergeAll()
        {
            foreach (var i in _mergeEntities)
            {
                var targetEntity = _mergeEntities.Get1(i).Target;
                var targetPosition = targetEntity.Get<Position>().value;
                
                var currentMergeEntity = _mergeEntities.GetEntity(i);
                var currentMergeCubeView = currentMergeEntity.Get<CubeViewRef>().Value;

                currentMergeEntity.Del<Selectable>();
                targetEntity.Del<Selectable>();
                
                currentMergeCubeView.transform.DOJump(targetPosition, 
                        _configuration.CubeMergeJumpPower, 1,
                        _configuration.CubeMergeDuration)
                    .OnComplete(() =>
                    {
                        //currentMergeCubeView.gameObject.SetActive(false);
                        _sceneData.CubePool.ReturnToPool(currentMergeCubeView);
                        currentMergeEntity.Destroy();
                        targetEntity.Get<Selectable>();
                        targetEntity.Get<OnMergeCompleteEvent>();
                    });
            }
        }
    }
}