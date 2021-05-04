using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.Entity.Events;

namespace MY.Scripts.Systems
{
    internal class GameOverCheckSystem : IEcsRunSystem, IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<CellViewRef> _cells = null;
        private readonly EcsFilter<CubeViewRef, Selectable>.Exclude<OnMergeEvent, OnMoveEvent> _cubes = null;
        private readonly EcsFilter<OnGameTimerFinishedEvent> _onGameTimerFinishedEvent = null;
        
        private readonly RuntimeData _runtimeData = null;

        private int _cellsCount;

        public void Init()
        {
            _cellsCount = _cells.GetEntitiesCount();
            _runtimeData.GameState.OnGameStateChange += OnGameStateChange;
        }

        private void OnGameStateChange(State state)
        {
            if (state == State.Game)
            {
                _cellsCount = _cells.GetEntitiesCount();
            }
        }

        public void Run()
        {
            if(_runtimeData.GameState.State != State.Game)
                return;
            
            if(_cellsCount == _cubes.GetEntitiesCount() || !_onGameTimerFinishedEvent.IsEmpty())
            {
                _runtimeData.GameState.State = State.GameOver;
            }
        }

        public void Destroy()
        {
            _runtimeData.GameState.OnGameStateChange -= OnGameStateChange;
        }
    }
}