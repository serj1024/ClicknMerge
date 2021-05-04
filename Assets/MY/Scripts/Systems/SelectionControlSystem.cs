using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.Entity.Events;

namespace MY.Scripts.Systems
{
    internal class SelectionControlSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Selectable, OnClickEvent>.Exclude<Selected> _newSelectEntities = null;

        public void Run()
        {
            AddNewSelectedEntities();
        }

        private void AddNewSelectedEntities()
        {  
            foreach (var i in _newSelectEntities)
            {
                SelectEntity(ref _newSelectEntities.GetEntity(i));
            }
        }

        private static void SelectEntity(ref EcsEntity entity)
        {
            entity.Get<Selected>();
            entity.Get<OnSelectedEvent>();
            entity.Del<Selectable>();
        }
    }
}