using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MY.Scripts.UnityComponents
{
    public class GameScreen : Screen
    {
        public TextMeshProUGUI TimerText;
        public TextMeshProUGUI PlayerScoreTextMultiplayerMode;
        public TextMeshProUGUI PlayerScoreTextEndlessMode;
        public TextMeshProUGUI PlayerNickname;
        public TextMeshProUGUI EndlessRecordText;
        public TextMeshProUGUI CurrentCash;
        public TextMeshProUGUI BotNickname;
        public TextMeshProUGUI PlayerRank;
        public TextMeshProUGUI BotRank;
        public TextMeshProUGUI BotScoreText;
        public BoostView autoMergeBoostView;
        public BoostView bombBoostView;
        public BoostView snailBoostView;
        public GameObject EndlessUpperPanel;
        public GameObject MultiplayerUpperPanel;
        public Image PlayerIconMultiplayerMode;
        public Image PlayerIconEndlessMode;
        public Image BotIcon;
    }
}