using System.Collections.Generic;
using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.Entity.Events;
using MY.Scripts.UnityComponents;

namespace MY.Scripts.Systems
{
    internal class AutoMergeBoostSystem : BoostSystem, IEcsRunSystem
    {
        private readonly EcsFilter<CubeViewRef, Position, Score>.Exclude<OnMergeEvent> _cubes = null;
        private readonly EcsFilter<AutoMergeIsRun> _autoMerge = null;
        private readonly EcsFilter<OnMergeCompleteEvent> _onMergeCompleteEvent = null;
        
        private readonly SceneData _sceneData = null;
        private readonly EcsWorld _world = null;
        private new readonly PlayerData _playerData = null;
        private readonly Configuration _configuration = null;

        public override void Init()
        {
            boostView = _sceneData.UI.GameScreen.autoMergeBoostView;
            base._playerData = _playerData;
            _cooldown = _configuration.CooldownAutoMergeBoost;
            base.Init();
        }

        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            if(!_canBoosting)
                return;
            if(!_autoMerge.IsEmpty())
                return;
                        
            _world.NewEntity().Get<AutoMergeIsRun>();
            AutoMerge();
        }

        public override void Run()
        {
            base.Run();
            if(_autoMerge.IsEmpty())
                return;
            if (!_onMergeCompleteEvent.IsEmpty())
                AutoMerge();
        }   

        private void AutoMerge()
        {
            var mergedCubes = new List<EcsEntity>();
            foreach (var i in _cubes)
            {
                foreach (var j in _cubes)
                {
                    ref var cubeEntity1 = ref _cubes.GetEntity(i);
                    ref var cubeEntity2 = ref _cubes.GetEntity(j);
                    var scoreNumber1 = _cubes.Get3(i).Value;
                    var scoreNumber2 = _cubes.Get3(j).Value;

                    if (cubeEntity1 == _cubes.GetEntity(j) || scoreNumber1 != scoreNumber2 ||
                        mergedCubes.Contains(cubeEntity1) || mergedCubes.Contains(cubeEntity2))
                        continue;
                    
                    cubeEntity1.Get<OnMergeEvent>().Target = cubeEntity2;
                    mergedCubes.Add(cubeEntity1);
                    mergedCubes.Add(cubeEntity2);
                    break;
                }
            }
            
            if (mergedCubes.Count == 0)
            {
                _autoMerge.GetEntity(0).Destroy();
            }
        }
    }
}