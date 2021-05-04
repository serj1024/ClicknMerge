using Leopotam.Ecs;
using MY.Scripts.UnityComponents;

namespace MY.Scripts.Systems
{
    internal class PlayerCashViewSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly PlayerData _playerData = null;
        private readonly RuntimeData _runtimeData = null;
        private readonly SceneData _sceneData = null;
        
        public void Init()
        {
            _playerData.ChangeCashValue += ChangeCashValue;
            _runtimeData.GameState.OnGameStateChange += ChangeCashValue;
            ChangeCashValue(_playerData.Cash);
        }

        private void ChangeCashValue(int cash)
        {
            switch (_runtimeData.GameState.State)
            {
                case State.Game:
                    _sceneData.UI.GameScreen.CurrentCash.SetText(cash.ToString());
                    break;
                case State.Menu:
                    _sceneData.UI.MenuScreen.PlayerCash.SetText(cash.ToString());
                    break;
            }
        }
        
        private void ChangeCashValue(State state)
        {
            ChangeCashValue(_playerData.Cash);
        }

        public void Destroy()
        {
            _playerData.ChangeCashValue -= ChangeCashValue;
            _runtimeData.GameState.OnGameStateChange -= ChangeCashValue;
        }
    }
}