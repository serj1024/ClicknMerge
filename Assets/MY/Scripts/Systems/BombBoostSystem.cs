using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.Entity.Events;
using MY.Scripts.Extensions;
using MY.Scripts.UnityComponents;

namespace MY.Scripts.Systems
{
    internal class BombBoostPlanningSystem : BoostSystem
    {
        private readonly EcsFilter<CubeViewRef, Position, Selectable>.Exclude<Selected, OnMergeEvent> _cubes = null;

        private readonly SceneData _sceneData = null;
        private new readonly PlayerData _playerData = null;
        private readonly Configuration _configuration = null;

        public override void Init()
        {
            boostView = _sceneData.UI.GameScreen.bombBoostView;
            base._playerData = _playerData;
            _cooldown = _configuration.CooldownBombBoost;
            base.Init();
        }

        protected override void OnButtonClick()
        {
            var countCubes = _cubes.GetEntitiesCount();
            var bombCubesCount = (int)(countCubes * 0.4f);
            
            if(bombCubesCount == 0)
                return;
            
            base.OnButtonClick();
            if(!_canBoosting)
                return;
            OnBombBoost(countCubes, bombCubesCount);
        }

        private void OnBombBoost(int countCubes, int bombCubesCount)
        {
            var random = new RandomWithoutRepeat(0, countCubes);
            for (var bombCubeCounter = 0; bombCubeCounter < bombCubesCount; bombCubeCounter++)
            {
                var indexCube = random.Next();
                _cubes.GetEntity(indexCube).Get<BombCube>();
            }
        }
    }
}