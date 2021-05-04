using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.Entity.Events;

namespace MY.Scripts.Systems
{
    internal class SelectionCubeViewSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CubeViewRef, OnSelectedEvent>.Exclude<OnDeselectEvent> _filterSelected = null;
        private readonly EcsFilter<CubeViewRef, OnDeselectEvent>.Exclude<OnSelectedEvent> _filterDeselected = null;
        
        public void Run()
        {
            foreach (var i in _filterSelected)
            {
                RenderSelect(_filterSelected.Get1(i), true);
            }
            foreach (var i in _filterDeselected)
            {
                RenderSelect(_filterDeselected.Get1(i), false);
            }
        }

        private void RenderSelect(CubeViewRef cubeViewRef, bool enable)
        {
            cubeViewRef.Value.SetSelectView(enable);
        }
    }
}