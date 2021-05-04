using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.Entity.Events;
using MY.Scripts.Extensions;
using MY.Scripts.UnityComponents;
using UnityEngine;

namespace MY.Scripts.Systems
{
    internal class TrainLogicSystem : IEcsRunSystem
    {
        private readonly EcsFilter<OnMergeCompleteEvent, Score> _onMergeCompleteEvent = null;
        private readonly EcsFilter<CubeViewRef, Position> _cubes = null;
        private readonly EcsFilter<OnClickEvent, Selected> _newSelectEntities = null;

        private readonly RuntimeData _runtimeData = null;
        private readonly EcsWorld _world = null;
        private readonly SceneData _sceneData = null;
        private readonly Configuration _configuration = null;

        private bool _canCubesMove = true;
        private int _countScoreToSecondStageTraining = 4;
        private int _countScoreToEndTraining = 30;
        private readonly Vector3Int _trainCubePosition1 = new Vector3Int(0, 0, 0);
        private readonly Vector3Int _trainCubePosition2 = new Vector3Int(4, 0, 1);
        private CubeView _trainCube2;
        private GameObject _clickAnimationObject;

        public void InitAnimationObject()
        {
            foreach (var i in _cubes)
            {
                var position = _cubes.Get2(i).value;
                var cube = _cubes.Get1(i).Value;

                if (position == _trainCubePosition2)
                    _trainCube2 = cube;

                if (position == _trainCubePosition1)
                    _clickAnimationObject =
                        Object.Instantiate(_configuration.ClickAnimationPrefab, cube.CanvasRectTransform);
                else
                    cube.Entity.Del<Selectable>();
            }
        }

        public void Run()
        {
            if (_runtimeData.GameState.State != State.Train)
                return;

            if (_clickAnimationObject == null && _trainCube2 == null)
                InitAnimationObject();

            if (!_newSelectEntities.IsEmpty())
            {
                if (_clickAnimationObject != null)
                {
                    _trainCube2.Entity.Get<Selectable>();
                    _clickAnimationObject.transform.SetParent(_trainCube2.CanvasRectTransform);
                    _clickAnimationObject.GetComponent<RectTransform>().localPosition = _configuration
                        .ClickAnimationPrefab.GetComponent<RectTransform>().localPosition;
                }
            }

            if (!_onMergeCompleteEvent.IsEmpty())
            {
                if (_clickAnimationObject != null)
                {
                    Object.Destroy(_clickAnimationObject.gameObject);
                    foreach (var i in _cubes)
                    {
                        _cubes.GetEntity(i).Get<Selectable>();
                    }
                }
            }

            var secondStageTraining = _runtimeData.PlayerScore >= _countScoreToSecondStageTraining;
            if (secondStageTraining && _canCubesMove)
            {
                _canCubesMove = false;
                _world.SendMessage(new OnCubeMoveTimedEvent());
            }

            if (secondStageTraining && !_onMergeCompleteEvent.IsEmpty())
                _sceneData.UI.TrainScreen.TrainText.SetText(
                    "Набери " + _countScoreToEndTraining + " очков. \n"
                    + "Набрано: " + _runtimeData.PlayerScore);

            if (_runtimeData.PlayerScore >= _countScoreToEndTraining)
                _runtimeData.GameState.State = State.EnterNickname;
        }
    }
}