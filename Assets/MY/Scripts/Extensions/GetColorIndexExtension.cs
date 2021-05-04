using UnityEngine;

namespace MY.Scripts.Extensions
{
    public static class GetColorIndexExtension
    {
            public static int GetColorIndex(this int scoreNumber, int LengthColorsData)
            {
                return (int)(Mathf.Log(scoreNumber) / 0.6931472f) % LengthColorsData;
                    //0.6931472 - > Mathf.Log(2) 
            }
    }
}