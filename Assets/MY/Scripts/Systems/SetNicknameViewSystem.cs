using Leopotam.Ecs;
using MY.Scripts.UnityComponents;
using UnityEngine;

namespace MY.Scripts.Systems
{
    internal class SetNicknamesViewSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly RuntimeData _runtimeData = null;
        private readonly SceneData _sceneData = null;
        private readonly PlayerData _playerData = null;

        public void Init()
        {
            _runtimeData.GameState.OnGameStateChange += OnGameStateChange;
            OnGameStateChange(_runtimeData.GameState.State);
        }

        public void Destroy()
        {
            _runtimeData.GameState.OnGameStateChange -= OnGameStateChange;
        }
        
        private void OnGameStateChange(State state)
        {
            switch (_runtimeData.GameState.State)
            {
                case State.Menu:
                    _sceneData.UI.MenuScreen.PlayerNickname.SetText(_playerData.Nickname);
                    break;
                case State.Loading:
                    _sceneData.UI.GameScreen.PlayerNickname.SetText(_playerData.Nickname);
                    var nicknameGenerator = new GameObject().AddComponent<NicknameGenerator>();
                    nicknameGenerator.Get(nickname =>
                    {
                        if(string.IsNullOrEmpty(nickname))
                            _runtimeData.GameState.State = State.InternetConnectionError;
                        else
                        {
                            _sceneData.UI.GameScreen.BotNickname.SetText(nickname);
                            _runtimeData.GameState.State = State.Game;
                        }
                    });
                    break;
            }
        }
    }
}