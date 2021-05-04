using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.Extensions;
using MY.Scripts.UnityComponents;
using UnityEngine;

namespace MY.Scripts.Systems
{
    internal class BombBoostExecuteSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BombCube, CubeViewRef, Position, Score> _bombCubes = null;
        private readonly SceneData _sceneData = null;
        private readonly Configuration _configuration = null;
        private readonly CubeColorsData _cubeColorsData = null;

        public void Run()
        {
            if(_bombCubes.IsEmpty()) 
                return;

            foreach (var i in _bombCubes)
            {
                ref var cubeView = ref _bombCubes.Get2(i).Value;
                _sceneData.CubePool.ReturnToPool(cubeView);
                ref var position = ref _bombCubes.Get3(i);
                var scoreNumber = _bombCubes.Get4(i).Value;
                var bombParticle = Object.Instantiate(_configuration.BombParticle,position.value, Quaternion.identity);
                var color = _cubeColorsData.Colors[scoreNumber.GetColorIndex(_cubeColorsData.Colors.Length)];
                bombParticle.GetComponent<ParticleSystemRenderer>().material .color = color;
                bombParticle.Play();
                _bombCubes.GetEntity(i).Destroy();
            }
        }
    }
}