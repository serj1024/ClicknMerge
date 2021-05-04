using MY.Scripts.Pooling;
using UnityEngine;

namespace MY.Scripts.UnityComponents
{
    internal class SceneData : MonoBehaviour
    {
        public Camera Camera;
        public UI UI;
        public CubePool CubePool;
        public GameObject CubeInMenu;

        private void OnValidate()
        {
            Camera = Camera.main;
        }
    }
}