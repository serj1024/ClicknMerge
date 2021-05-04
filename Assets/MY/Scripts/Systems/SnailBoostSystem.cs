using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.UnityComponents;

namespace MY.Scripts.Systems
{
    internal class SnailBoostSystem : BoostSystem
    {
        private readonly EcsFilter<CubeMoveTime> _filter = null;
        
        private readonly SceneData _sceneData = null;
        private readonly Configuration _configuration = null;
        private new readonly PlayerData _playerData = null;
        
        public override void Init()
        {
            boostView = _sceneData.UI.GameScreen.snailBoostView;
            base._playerData = _playerData;
            _cooldown = boostWorkingTime();
            base.Init();
        }

        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            if(!_canBoosting)
                return;
            OnSnailBoost();
        }

        private void OnSnailBoost()
        {
            if(!_filter.IsEmpty())
                _filter.Get1(0).Value = boostWorkingTime();
        }

        private float boostWorkingTime()
        {
            return _configuration.StartMoveSpeedByRank[_playerData.Rank] * 3;
        }
    }
}