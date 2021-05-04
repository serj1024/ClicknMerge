using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.Entity.Events;
using MY.Scripts.Extensions;
using MY.Scripts.UnityComponents;
using UnityEngine;

namespace MY.Scripts.Systems
{
    internal class SpawnCubeOnFirstCellsSystem : IEcsRunSystem, IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<CellViewRef, Position> _cellsFilter = null;
        private readonly EcsFilter<OnCubeMoveTimedEvent> _onCubeMoveTimedEvent = null;
        private readonly EcsFilter<CubeViewRef, Position>.Exclude<OnMergeEvent> _cubes = null;
        private readonly EcsFilter<AutoMergeIsRun> _automerge = null;
        
        private readonly Configuration _configuration = null;
        private readonly EcsWorld _world = null;
        private readonly CubeColorsData _cubeColorsData = null;
        private readonly SceneData _sceneData = null;
        private readonly RuntimeData _runtimeData = null;

        public void Init()
        {
            _runtimeData.GameState.OnGameStateChange += OnGameStateChange;
            CreateCubs(0);    
            CreateCubs(1);    
        }

        private void OnGameStateChange(State obj)
        {
            CreateCubs(0);    
            CreateCubs(1);   
        }

        public void Run()
        {
            if(_runtimeData.GameState.State != State.Game && _runtimeData.GameState.State != State.Train)
                return;
            
            if (_onCubeMoveTimedEvent.IsEmpty())
                return;
            if(!_automerge.IsEmpty())
                return;

            CreateCubs(0);
        }

        private void CreateCubs(int rowIndex)
        {
            foreach (var i in _cellsFilter)
            {
                var cellPosition = _cellsFilter.Get2(i);

                if (cellPosition.value.z == rowIndex && !ContainCube(cellPosition))
                {
                    var cubeView = _sceneData.CubePool.Get(_configuration.CubeView);
                    cubeView.transform.position = cellPosition.value;
                    
                    CreateCubeEntity(cellPosition.value, cubeView, 1);
                    
                    cubeView.gameObject.SetActive(true);
                }
            }
        }
        
        public void CreateCubeEntity(Vector3Int position, CubeView cubeView, int scoreNumber)
        {
            var cubeEntity = _world.NewEntity();
            cubeEntity.Get<Position>().value = position;
            cubeEntity.Get<Selectable>();
            cubeEntity.Get<CubeViewRef>().Value = cubeView;
        
            cubeEntity.Get<Score>().Value = scoreNumber;
            cubeView.SetScoreText(scoreNumber.ToString());
            var color =  _cubeColorsData.Colors[scoreNumber.GetColorIndex(_cubeColorsData.Colors.Length)];
            cubeView.SetMaterialColor(color);
        
            cubeView.Entity = cubeEntity;
        } 

        private bool ContainCube(Position position)
        {
            foreach (var i in _cubes)
            {
                if (_cubes.Get2(i).value == position.value)
                {
                    return true;
                }
            }
            return false;
        }

        public void Destroy()
        {
            _runtimeData.GameState.OnGameStateChange -= OnGameStateChange;
        }
    }
}