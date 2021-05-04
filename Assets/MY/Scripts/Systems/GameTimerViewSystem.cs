using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.UnityComponents;
using TMPro;
using UnityEngine;

namespace MY.Scripts.Systems
{
    internal class GameTimerViewSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GameTimer> _timer = null;
        private readonly SceneData _sceneData = null;
        
        public void Run()
        {
            if (_timer.IsEmpty())
                return;
            
            DisplayTime(_timer.Get1(0).Value, _sceneData.UI.GameScreen.TimerText);
        }
        
        void DisplayTime(float timeToDisplay, TextMeshProUGUI timeText)
        {
            timeToDisplay += 1;

            float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}