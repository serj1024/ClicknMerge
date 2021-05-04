using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.UnityComponents;
using UnityEngine;

namespace MY.Scripts.Systems
{
    internal class CreateCellSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;
        
        
        public void Init()
        {
            _runtimeData.GameState.OnGameStateChange += OnGameStateChange;
            if(_runtimeData.GameState.State != State.Game && _runtimeData.GameState.State != State.Train)
                return;
            
            CreateCells();
        }

        private void OnGameStateChange(State state)
        {
            if (state == State.Game)
            {
                CreateCells();
            }
        }
        
        private void CreateCells()
        {
            var parent = new GameObject {name = "Cells"}.transform;

            for (int x = 0; x < _configuration.LevelWidth; x++)
            {
                for (int z = 0; z < _configuration.LevelHeight; z++)
                {
                    var position = new Vector3Int(x, 0, z);

                    var cellView = Object.Instantiate(
                        _configuration.CellView,
                        position,
                        Quaternion.Euler(90, 0, 0),
                        parent
                    );
                    CreateCellEntity(position, cellView);
                }
            }
        }

        private void CreateCellEntity(Vector3Int position, CellView cellView)
        {
            var cellEntity = _world.NewEntity();
            cellEntity.Get<CellViewRef>().Value = cellView;
            cellEntity.Get<Position>().value = position;
        }
    }
}