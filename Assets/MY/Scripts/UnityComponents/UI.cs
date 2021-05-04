using System.Collections.Generic;
using UnityEngine;

namespace MY.Scripts.UnityComponents
{
    public class UI : MonoBehaviour
    {
        public GameOverScreen GameOverScreen;
        public GameScreen GameScreen;
        public TrainScreen TrainScreen;
        public EnterNicknameScreen EnterNicknameScreen;
        public MenuScreen MenuScreen;
        public LoadingScreen LoadingScreen;
        public InternetConnectionErrorScreen InternetConnectionErrorScreen;
        
        public Dictionary<State, Screen> screens = new Dictionary<State, Screen>();

        private void Awake()
        {
            screens = new Dictionary<State, Screen>()
            {
                {State.GameOver, GameOverScreen },
                {State.Game, GameScreen },
                {State.Train, TrainScreen},
                {State.EnterNickname, EnterNicknameScreen},
                {State.Menu, MenuScreen},
                {State.Loading, LoadingScreen},
                {State.InternetConnectionError, InternetConnectionErrorScreen}
            };
        }
        public void OnGameStateChange(State state)
        {
            foreach (KeyValuePair<State, Screen> screen in screens)
            {
                if (screen.Key != state)
                    screen.Value.Show(false);
                else
                    screen.Value.Show(true);
            }
        }
    }
}