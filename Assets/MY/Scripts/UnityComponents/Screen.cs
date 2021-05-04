using UnityEngine;

namespace MY.Scripts.UnityComponents
{
    [RequireComponent(typeof(Canvas))]
    public class Screen : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private void OnValidate()
        {
            _canvas = GetComponent<Canvas>();
        }

        public void Show(bool enable)
        {
            _canvas.enabled = enable;
            gameObject.SetActive(enable);
        }
    }
}