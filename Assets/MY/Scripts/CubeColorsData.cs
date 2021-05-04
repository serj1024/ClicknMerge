using UnityEngine;

namespace MY.Scripts
{
    [CreateAssetMenu(menuName = "My Setting/Cube Colors", fileName = "Cube Colors", order = 0)]
    public class CubeColorsData : ScriptableObject
    {
        public Color[] Colors;
    }
}