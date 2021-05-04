using DG.Tweening;
using UnityEngine;

namespace MY.Scripts.Extensions
{
    [RequireComponent(typeof(CanvasGroup))]
    public class TextBlink : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private float _endValueFade;
        [SerializeField] private float _durationFade;

        private void OnValidate()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            _canvasGroup.DOFade(_endValueFade, _durationFade).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
