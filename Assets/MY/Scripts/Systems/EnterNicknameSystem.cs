using System;
using System.Linq;
using System.Text.RegularExpressions;
using Leopotam.Ecs;
using MY.Scripts.Entity;
using MY.Scripts.UnityComponents;
using TMPro;
using Object = UnityEngine.Object;

namespace MY.Scripts.Systems
{
    internal class EnterNicknameSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<CubeViewRef> _cubes = null;
        private readonly EcsFilter<CellViewRef> _cells = null;
        
        private readonly SceneData _sceneData = null;
        private readonly RuntimeData _runtimeData = null;
        private readonly PlayerData _playerData = null;

        private TMP_InputField _inputField;
        private readonly Regex _regex = new Regex(@"[0-9a-zA-F]");

        public void Init()
        {
            _inputField = _sceneData.UI.EnterNicknameScreen.InputField;
            _inputField.onEndEdit.AddListener(OnEndEditNickname);
            _inputField.onValueChanged.AddListener(OnNicknameValueChange);

            _runtimeData.GameState.OnGameStateChange += OnGameStateChange;
        }

        private void OnNicknameValueChange(string text)
        {
            if (text != "" && !_regex.IsMatch(text.Last().ToString()))
            {
                var newText = String.Join("", text.Remove(text.Length - 1));
                _inputField.text = newText;
            }
        }

        private void OnEndEditNickname(string text)
        {
            if (text.Length > 2)
            {
                _playerData.IsTrainedCompleted = true;
                _playerData.Nickname = text;
                _runtimeData.GameState.State = State.Menu;
            }
        }

        private void OnGameStateChange(State state)
        {
            if (state == State.EnterNickname)
            {
                foreach (var i in _cells)
                {
                    Object.Destroy(_cells.Get1(i).Value.gameObject);
                    _cells.GetEntity(i).Destroy();
                }
                foreach (var i in _cubes)
                {
                    _sceneData.CubePool.ReturnToPool(_cubes.Get1(i).Value);
                    _cubes.GetEntity(i).Destroy();
                }
            }
        }


        public void Destroy()
        {
            _runtimeData.GameState.OnGameStateChange -= OnGameStateChange;
            _inputField.onEndEdit.RemoveListener(OnEndEditNickname);
            _inputField.onValueChanged.RemoveListener(OnNicknameValueChange);
        }
    }
}