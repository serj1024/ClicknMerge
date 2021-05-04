using System;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;

namespace MY.Scripts.UnityComponents
{
    [SelectionBase]
    public class CubeView : MonoBehaviour
    {
        public EcsEntity Entity;
        
        [SerializeField] private TextMeshPro _scoreText;
        [SerializeField] private Renderer _cubeRenderer;
        [SerializeField] private Animator _animator;
        public RectTransform CanvasRectTransform;
        
        private MaterialPropertyBlock _materialPropertyBlock;

        private void OnValidate()
        {
            CanvasRectTransform = GetComponentInChildren<Canvas>().GetComponent<RectTransform>();
            _scoreText = GetComponentInChildren<TextMeshPro>();
            _cubeRenderer = GetComponentInChildren<Renderer>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void Awake()
        {
            _materialPropertyBlock = new MaterialPropertyBlock();
        }

        private void OnEnable()
        {
             if(_animator != null)
                 _animator.SetTrigger("Enable");
        }

        private void OnDisable()
        {
            transform.localScale = Vector3.one;
            SetSelectView(false);
        }

        public void SetScoreText(string text)
        {
            _scoreText.SetText(text);
        }

        public void SetMaterialColor(Color color)
        {
            _materialPropertyBlock.SetColor("Color_9b5f621908834689aaa1fb97d83f6b76", color);
            _cubeRenderer.SetPropertyBlock(_materialPropertyBlock);
        }

        public void SetSelectView(bool enable)
        {
            _materialPropertyBlock.SetFloat("Boolean_2366b3e0cb9349cea98b7e0df8fe50e8", Convert.ToInt32(enable));
            _cubeRenderer.SetPropertyBlock(_materialPropertyBlock);
        }
    }
}