using System;

namespace MY.Scripts
{
    internal class GameState
    {
        public Action<State> OnGameStateChange;
        
        private State _state;
        public GameState(State state)
        {
            State = state;
        }
        
        public State State
        {
            get => _state;
            set
            {
                _state = value;
                OnGameStateChange?.Invoke(_state);
            }
        }
    }
    public enum State
    {
        GameOver,
        Game,
        Train,
        EnterNickname,
        Menu,
        Loading,
        InternetConnectionError
    }
}