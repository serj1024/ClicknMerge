using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.Entity.Events;
using MY.Scripts.Extensions;

namespace MY.Scripts.Systems
{
    internal class DeselectionControlSystem : IEcsRunSystem
    {
        private readonly EcsFilter<OnClickEvent> _onClickEvent = null;
        private readonly EcsFilter<Selected, Score> _selectedEntities = null;
        
        private readonly EcsWorld _world = null;
        
        public void Run()
        {
            if (_onClickEvent.IsEmpty())
                return;
            if(!_selectedEntities.AllEntitiesEquals<Score>())
            {
                DeselectAllEntities();
                _world.SendMessage(new OnWrongSelectEvent());
                return;
            }

            if (_selectedEntities.GetEntitiesCount() != 2) 
                return;
            
            _selectedEntities.GetEntity(0).Get<OnMergeEvent>()
                .Target = _selectedEntities.GetEntity(1);
            DeselectAllEntities();
        }
        private void DeselectAllEntities()
        {
            foreach (var i in _selectedEntities)
            {
                DeselectEntity(ref _selectedEntities.GetEntity(i));
            }
        }
        
        private void DeselectEntity(ref EcsEntity selectedEntity)
        {
            selectedEntity.Del<Selected>();
            selectedEntity.Get<OnDeselectEvent>();
            selectedEntity.Get<Selectable>();
        }
    }
}