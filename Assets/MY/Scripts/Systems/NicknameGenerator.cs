using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

namespace MY.Scripts.Systems
{
    internal class NicknameGenerator : MonoBehaviour
    {
        private static string serviceURl = @"https://uzby.com/api.php?min=4&max=12";
        private string _nickname;

        public void Get(Action<string> onCompleted)
        {
            StartCoroutine(LoadFromServer(serviceURl, onCompleted));
        }

        IEnumerator LoadFromServer(string url, Action<string> onCompleted)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();
                _nickname = webRequest.downloadHandler.text;

                if (_nickname.Length <= 6 && Random.Range(0,2) == 1)
                    _nickname += Random.Range(1,999);
                
                onCompleted?.Invoke(_nickname);
            }
        }
    }
}