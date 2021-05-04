using Leopotam.Ecs;
using MY.Scripts.UnityComponents;
using UnityEngine;

namespace MY.Scripts.Systems
{
    internal class SetCameraConstantWidthSystem : IEcsInitSystem, IEcsRunSystem
    {
        Vector2 DefaultResolution = new Vector2(1080, 1920);
        private readonly Configuration _configuration = null;
        private readonly SceneData _sceneData = null;
        private Camera componentCamera;
    
        private float initialSize;
        private float targetAspect;

        private float initialFov;
        private float horizontalFov = 120f;

        public void Init()
        {
            componentCamera = _sceneData.Camera;
            initialSize = componentCamera.orthographicSize;

            targetAspect = DefaultResolution.x / DefaultResolution.y;

            initialFov = componentCamera.fieldOfView;
            horizontalFov = CalcVerticalFov(initialFov, 1 / targetAspect);
        }

        public void Run()
        {
            if (componentCamera.orthographic)
            {
                float constantWidthSize = initialSize * (targetAspect / componentCamera.aspect);
                componentCamera.orthographicSize = Mathf.Lerp(constantWidthSize, initialSize, _configuration.WidthOrHeight);
            }
            else
            {
                float constantWidthFov = CalcVerticalFov(horizontalFov, componentCamera.aspect);
                componentCamera.fieldOfView = Mathf.Lerp(constantWidthFov, initialFov, _configuration.WidthOrHeight);
            }
        }

        private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
        {
            float hFovInRads = hFovInDeg * Mathf.Deg2Rad;

            float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);

            return vFovInRads * Mathf.Rad2Deg;
        }
    }
}