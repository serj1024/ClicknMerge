using UnityEngine;

namespace MY.Scripts.Extensions
{
    public class AnimationRotateAroundLocal_Z : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void Update()
        {
            transform.Rotate(0, _speed * Time.deltaTime, 0); 
        }
    }
}