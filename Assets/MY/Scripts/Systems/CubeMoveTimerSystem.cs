using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.Entity.Events;
using MY.Scripts.Extensions;
using UnityEngine;

namespace MY.Scripts.Systems
{
    internal class CubeMoveTimerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<CubeMoveTime> _filter = null;
        private readonly EcsWorld _world = null;
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;
        private readonly EcsFilter<AutoMergeIsRun> _automerge = null;
        private readonly PlayerData _playerData = null;

        private int _moveCounter = 0;

        public void Run()
        {
            if (_runtimeData.GameState.State != State.Game)
                return;
            if (!_automerge.IsEmpty())
                return;

            foreach (var i in _filter)
            {
                ref var cubeMoveTime = ref _filter.Get1(i);
                cubeMoveTime.Value -= Time.deltaTime;

                if (cubeMoveTime.Value <= 0)
                {
                    _moveCounter++;
                    _world.SendMessage(new OnCubeMoveTimedEvent());

                    cubeMoveTime.Value = _configuration.StartMoveSpeedByRank[_playerData.Rank] -
                                         _configuration.CubesMoveTimeStep * _moveCounter;
                    if (cubeMoveTime.Value < _configuration.CubesMoveIntervalTimeMin)
                        cubeMoveTime.Value = _configuration.CubesMoveIntervalTimeMin;
                }
            }
        }

        public void Init()
        {
            _world.NewEntity().Get<CubeMoveTime>().Value = _configuration.StartMoveSpeedByRank[_playerData.Rank];
        }
    }
}