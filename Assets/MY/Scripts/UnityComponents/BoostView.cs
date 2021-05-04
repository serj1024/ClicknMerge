using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MY.Scripts.UnityComponents
{
    public class BoostView : MonoBehaviour
    {
        public Button Button;
        public TextMeshProUGUI CostText;
        public int Cost;

        private void OnValidate()
        {
            Button = GetComponent<Button>();
            CostText = GetComponentInChildren<TextMeshProUGUI>();
            CostText.SetText(Cost.ToString());
        }
    }
}