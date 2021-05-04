using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MY.Scripts.UnityComponents
{
    public class MenuScreen : Screen
    {
        public TextMeshProUGUI PlayerNickname;
        public TextMeshProUGUI PlayerCash;
        public TextMeshProUGUI PlayerRank;
        public Button StartGameButton;
        public GameObject SelectGameModePanel;
        public Button FakeMultiplayerModeButton;
        public Button EndlessModeButton;
        public Image PlayerIcon;
    }
}