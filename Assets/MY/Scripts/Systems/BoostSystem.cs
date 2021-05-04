using Leopotam.Ecs;
using MY.Scripts.UnityComponents;
using UnityEngine;

namespace MY.Scripts.Systems
{
    internal class BoostSystem : IEcsInitSystem, IEcsDestroySystem, IEcsRunSystem
    {
        protected BoostView boostView;
        protected PlayerData _playerData;
        protected bool _canBoosting;
        protected float _cooldown;
        private float _interactableTimer;
        
        public virtual void Init()
        {
            boostView.Button.onClick.AddListener(OnButtonClick);
            _interactableTimer = _cooldown;
        }
        
        public void Destroy()
        {
            boostView.Button.onClick.RemoveListener(OnButtonClick);
        }

        public virtual void Run()
        {
            _interactableTimer -= Time.deltaTime;
            if (_interactableTimer < 0)
            {
                boostView.Button.interactable = true;
                _interactableTimer = _cooldown;
            }   
        }
        
        protected virtual void OnButtonClick()
        {
            if (boostView.Cost > _playerData.Cash)
            {
                _canBoosting = false;
                return;
            }
            _canBoosting = true;
            _playerData.Cash -= boostView.Cost;
            _interactableTimer = _cooldown;
            boostView.Button.interactable = false;
        }
    }
}