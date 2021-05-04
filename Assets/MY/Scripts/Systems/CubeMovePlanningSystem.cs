using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.Entity.Events;
using UnityEngine;

namespace MY.Scripts.Systems
{
    internal class CubeMovePlanningSystem : IEcsRunSystem
    {
        private readonly EcsFilter<OnCubeMoveTimedEvent> _onCubeMoveTimedEvent = null;
        private readonly EcsFilter<CubeViewRef, Position> _cubes = null;
        private readonly EcsFilter<CellViewRef, Position> _cellsFilter = null;

        public void Run()
        {
            if(_onCubeMoveTimedEvent.IsEmpty())
                return;
            
            foreach (var i in _cubes)
            {
                var cubeEntity = _cubes.GetEntity(i);
                var cubePosition = cubeEntity.Get<Position>();
                var nextCubePosition = new Position {value = cubePosition.value + Vector3Int.forward};
                if (HasFreeCell(cubePosition))
                {
                    cubeEntity.Get<OnMoveEvent>().TargetPosition = nextCubePosition;   
                }   
            }
        }

        private bool HasFreeCell(Position currentCubePosition)
        {
            foreach (var cell in _cellsFilter)
            {   
                var freeCell = true;
                var cellPos = _cellsFilter.Get2(cell).value;
                foreach (var cube in _cubes)
                {
                    var cubePos = _cubes.Get2(cube).value;
                    if (cellPos == cubePos)
                    {
                        freeCell = false;
                        break;
                    }
                }
                if (freeCell && cellPos.x == currentCubePosition.value.x && cellPos.z > currentCubePosition.value.z)
                    return true;
            }
            return false;
        }
    }
}