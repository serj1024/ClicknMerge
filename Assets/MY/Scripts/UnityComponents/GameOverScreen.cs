using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MY.Scripts.UnityComponents
{
    public class GameOverScreen : Screen
    {
        public TextMeshProUGUI CashTMP;
        public TextMeshProUGUI TitleTMP;
        public TextMeshProUGUI ScoreTMP;
        public Button BackToMenuButton;
        public Button ShowRewardedAdButton;
        public int EnableBackToMenuButtonTime = 3;
        public GameObject NewRankPanel;
        public Image RankIcon;
        public TextMeshProUGUI RankUpTMP;

        private void Awake()
        {
            BackToMenuButton.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            BackToMenuButton.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            StartCoroutine(EnableBackToMenuButton());
        }

        IEnumerator EnableBackToMenuButton()
        {
            yield return new WaitForSeconds(EnableBackToMenuButtonTime);
            BackToMenuButton.gameObject.SetActive(true);
        }
    }
}