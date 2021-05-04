using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MY.Scripts.UnityComponents
{
    public class InternetConnectionErrorScreen : Screen
    {
        public Button BackToMenuButton;

        private void OnEnable()
        {
            BackToMenuButton.onClick.AddListener(BackToMenu);
        }

        private void OnDisable()
        {
            BackToMenuButton.onClick.RemoveListener(BackToMenu);
        }

        private void BackToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}