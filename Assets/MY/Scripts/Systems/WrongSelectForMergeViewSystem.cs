using DG.Tweening;
using Leopotam.Ecs;
using MY.Scripts.Entity.Events;
using MY.Scripts.UnityComponents;

namespace MY.Scripts.Systems
{
    internal class WrongSelectForMergeViewSystem : IEcsRunSystem
    {
        private readonly EcsFilter<OnWrongSelectEvent> _filter = null;
        private readonly SceneData _sceneData = null;
        private readonly Configuration _configuration = null;
        
        public void Run()
        {
            if (_filter.IsEmpty())
                return;
            
            Vibration.Vibrate(_configuration.DeviceVibrateInMilliseconds);
            _sceneData.Camera.DOShakePosition(_configuration.CameraShakeDuration,
                _configuration.CameraShakeStrength,
                _configuration.CameraShakeVibrato,
                _configuration.CameraShakeRandomness,
                _configuration.CameraShakeFadeOut);
        }
    }
}