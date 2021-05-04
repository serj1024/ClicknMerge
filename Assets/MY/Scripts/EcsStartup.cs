using Leopotam.Ecs;
using MY.Scripts.Entity.Events;
using MY.Scripts.Systems;
using MY.Scripts.UnityComponents;
using UnityEngine;

namespace MY.Scripts
{
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private SceneData _sceneData;
        [SerializeField] private Configuration _configuration;
        [SerializeField] private CubeColorsData _cubeColorsData;
        [SerializeField] private State _debugGameState;
        
        private RuntimeData _runtimeData;
        private PlayerData _playerData;
        private EcsWorld _world;
        private EcsSystems _systems;

        void Start()
        {
            // void can be switched to IEnumerator for support coroutines.

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _playerData = new PlayerData(_configuration);
            _runtimeData = _playerData.IsTrainedCompleted ? new RuntimeData(State.Menu) : new RuntimeData(State.Train);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
            _runtimeData = new RuntimeData(_debugGameState);
#endif
            _systems
                .Add(new CreateCellSystem())
                .Add(new SetCameraConstantWidthSystem())

                .Add(new EnterNicknameSystem())
                .Add(new SetNicknamesViewSystem())
                .Add(new MenuLogicSystem())
                .Add(new RankViewSystem())
                
                .Add(new AutoMergeBoostSystem())
                .Add(new BombBoostPlanningSystem())
                .Add(new BombBoostExecuteSystem())
                .Add(new SnailBoostSystem())
                
                .Add(new InputControlSystem())
                .Add(new SelectionControlSystem())
                .Add(new DeselectionControlSystem())
                .Add(new MergeSystem())
                
                .Add(new WrongSelectForMergeViewSystem())
                .Add(new SelectionCubeViewSystem())

                .Add(new UpdateCubeScoreSystem())
                .Add(new UpdateCubeScoreViewSystem())
                
                .Add(new TrainLogicSystem())
                
                .Add(new CubeMoveTimerSystem())
                .Add(new CubeMovePlanningSystem())
                .Add(new CubeMoveExecuteSystem())
                
                .Add(new PlayerScoringSystem())
                .Add(new BotScoringSystem())
                .Add(new ScoringViewSystem())
             
                .Add(new SpawnCubeOnFirstCellsSystem())
                
                .Add(new GameTimerSystem())
                .Add(new GameTimerViewSystem())
                
                .Add(new GameOverCheckSystem())
                .Add(new RankUpgradeSystem())
                .Add(new CalculateCashSystem())
                .Add(new ShowRewardedAdSystem())
                .Add(new PlayerCashViewSystem())
                .Add(new GameOverViewSystem())

                .OneFrame<OnCubeMoveTimedEvent>()
                .OneFrame<OnClickEvent>()
                .OneFrame<OnDeselectEvent>()
                .OneFrame<OnSelectedEvent>()
                .OneFrame<OnMoveEvent>()
                .OneFrame<OnMergeEvent>()
                .OneFrame<OnMergeCompleteEvent>()
                .OneFrame<OnWrongSelectEvent>()
                .OneFrame<OnGameTimerFinishedEvent>()
                .OneFrame<OnShowRewardedAdEvent>()
                
                .Inject(_configuration)
                .Inject(_sceneData)
                .Inject(_cubeColorsData)
                .Inject(_runtimeData)
                .Inject(_playerData)
                .Init();
            
            _sceneData.CubePool.Prewarm(36, _configuration.CubeView);
            _runtimeData.GameState.OnGameStateChange += _sceneData.UI.OnGameStateChange;
            _sceneData.UI.OnGameStateChange(_runtimeData.GameState.State);
        }

        void Update()
        {
            _systems?.Run();
        }

        void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
            
            _runtimeData.GameState.OnGameStateChange -= _sceneData.UI.OnGameStateChange;
        }
    }
}