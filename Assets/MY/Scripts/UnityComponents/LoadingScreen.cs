using System.Collections;
using TMPro;
using UnityEngine;

namespace MY.Scripts.UnityComponents
{
    public class LoadingScreen : Screen
    {
        public TextMeshProUGUI LoadingTMP;
        private string initLoadingText;

        private void OnEnable()
        {
            StartCoroutine(AnimateTMP());
            initLoadingText = LoadingTMP.text;
        }

        private IEnumerator AnimateTMP()
        {
            int pointCounter = 0;
            for (;;)
            {
                yield return new WaitForSeconds(0.5f);
                pointCounter++;
                if (pointCounter > 3)
                {
                    pointCounter = 0;
                    LoadingTMP.SetText(initLoadingText);
                }
                else
                {
                    LoadingTMP.SetText(LoadingTMP.text + ".");
                }
            }
        }
    }
}