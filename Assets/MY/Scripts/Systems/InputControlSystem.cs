using Leopotam.Ecs;
using MY.Scripts.Entity.Events;
using MY.Scripts.UnityComponents;
using UnityEngine;

namespace MY.Scripts.Systems
{
    internal class InputControlSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly SceneData _sceneData = null;
        private readonly RuntimeData _runtimeData = null;
        
        private Camera camera;

        public void Run()
        {
            var correctlyGameState = _runtimeData.GameState.State == State.Game || _runtimeData.GameState.State == State.Train;
            if (correctlyGameState && Input.GetMouseButtonDown(0))
            {
                var ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hitInfo))
                {
                    if (hitInfo.collider.TryGetComponent<CubeView>(out var cubeView))
                    {
                        cubeView.Entity.Get<OnClickEvent>();
                    }
                }
            }
        }

        public void Init()
        {
            camera = _sceneData.Camera;
            Input.multiTouchEnabled = false;
        }
    }
}