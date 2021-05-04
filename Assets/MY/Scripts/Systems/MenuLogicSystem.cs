using Leopotam.Ecs;
using MY.Scripts.UnityComponents;

namespace MY.Scripts.Systems
{
    internal class MenuLogicSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly RuntimeData _runtimeData = null;
        private readonly SceneData _sceneData = null;
        private readonly PlayerData _playerData = null;

        public void Init()
        {
            _runtimeData.GameState.OnGameStateChange += OnGameStateChange;
            _sceneData.UI.MenuScreen.StartGameButton.onClick.AddListener(ShowSelectGameModePanel);
            _sceneData.UI.MenuScreen.FakeMultiplayerModeButton.onClick.AddListener(SelectFakeMultiplayerMode);
            _sceneData.UI.MenuScreen.EndlessModeButton.onClick.AddListener(SelectEndlessMode);
            InitMenu();
        }

        private void SelectEndlessMode()
        {
            _runtimeData.GameMode = GameMode.Endless;
            _sceneData.UI.GameScreen.EndlessUpperPanel.SetActive(true);
            _sceneData.UI.GameScreen.MultiplayerUpperPanel.SetActive(false);
            _sceneData.UI.GameScreen.EndlessRecordText.SetText(_playerData.EndlessRecord.ToString());
            _runtimeData.GameState.State = State.Game;
        }

        private void SelectFakeMultiplayerMode()
        {
            _runtimeData.GameMode = GameMode.FakeMultiplayer;
            _sceneData.UI.GameScreen.EndlessUpperPanel.SetActive(false);
            _sceneData.UI.GameScreen.MultiplayerUpperPanel.SetActive(true);
            _runtimeData.GameState.State = State.Loading;
        }

        private void ShowSelectGameModePanel()
        {
            _sceneData.UI.MenuScreen.SelectGameModePanel.SetActive(true);
            _sceneData.UI.MenuScreen.StartGameButton.gameObject.SetActive(false);
        }

        private void OnGameStateChange(State state)
        {
            InitMenu();
        }

        private void InitMenu()
        {
             _sceneData.CubeInMenu.SetActive(_runtimeData.GameState.State == State.Menu);
             _sceneData.UI.MenuScreen.SelectGameModePanel.SetActive(false);
             _sceneData.UI.MenuScreen.StartGameButton.gameObject.SetActive(true);
        }

        public void Destroy()
        {
            _runtimeData.GameState.OnGameStateChange -= OnGameStateChange;
            _sceneData.UI.MenuScreen.StartGameButton.onClick.RemoveListener(ShowSelectGameModePanel);
            _sceneData.UI.MenuScreen.FakeMultiplayerModeButton.onClick.RemoveListener(SelectFakeMultiplayerMode);
            _sceneData.UI.MenuScreen.EndlessModeButton.onClick.RemoveListener(SelectEndlessMode);
        }
    }
}