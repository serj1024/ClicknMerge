using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.Entity.Events;
using MY.Scripts.Extensions;
using UnityEngine;

namespace MY.Scripts.Systems
{
    internal class GameTimerSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<GameTimer> _filter = null;
        private readonly EcsWorld _world = null;
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;
        
        public void Init()
        {
            _world.NewEntity().Get<GameTimer>().Value = _configuration.GameTime;
        }

        public void Run()
        {
            if(_runtimeData.GameState.State != State.Game)
                return;
            if(_runtimeData.GameMode == GameMode.Endless)
                return;
            
            foreach (var i in _filter)
            {
                ref var gameTimer = ref _filter.Get1(i);
                gameTimer.Value -= Time.deltaTime;
                
                if (gameTimer.Value < 0)
                {
                    _world.SendMessage(new OnGameTimerFinishedEvent());
                }
            }
        }
    }
}