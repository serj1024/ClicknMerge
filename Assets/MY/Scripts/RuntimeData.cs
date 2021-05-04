using System;
using MY.Scripts.Systems;

namespace MY.Scripts
{
    internal class RuntimeData
    {
        public Action OnChangeScore;
        public GameState GameState;
        public GameMode GameMode;
        public int CalculatedCash;

        private int _botScore;
        private int _playerScore;

        public int BotScore
        {
            get => _botScore;
            set
            {
                _botScore = value;
                OnChangeScore?.Invoke();
            }
        }

        public int PlayerScore
        {
            get => _playerScore;
            set
            {
                _playerScore = value;
                OnChangeScore?.Invoke();
            }
        }

        public RuntimeData(State gameState)
        {
            GameState = new GameState(gameState);
        }
    }
}