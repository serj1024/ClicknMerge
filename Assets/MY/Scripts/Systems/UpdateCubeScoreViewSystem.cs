using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.Entity.Events;
using MY.Scripts.Extensions;

namespace MY.Scripts.Systems
{
    internal class UpdateCubeScoreViewSystem : IEcsRunSystem
    {
        private readonly EcsFilter<OnMergeCompleteEvent, Score, CubeViewRef> _filter = null;
        private readonly CubeColorsData _cubeColorsData = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                var scoreNumber = _filter.Get2(i).Value;
                SetCubeScoreText(i, scoreNumber);
                SetCubeColor(i, scoreNumber);
            }
        }

        private void SetCubeColor(int i, int scoreNumber)
        {
            var color = _cubeColorsData.Colors[scoreNumber.GetColorIndex(_cubeColorsData.Colors.Length)];
            _filter.Get3(i).Value.SetMaterialColor(color);
        }
        

        private void SetCubeScoreText(int i, int scoreNumber)
        {
            _filter.Get3(i).Value.SetScoreText(scoreNumber.ToString());
        }
    }
}