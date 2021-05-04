using System;
using UnityEngine;

namespace MY.Scripts
{
    internal class PlayerData
    {
        public Action<int> ChangeCashValue;

        private readonly Configuration _configuration;

        public PlayerData(Configuration configuration)
        {
            _configuration = configuration;
        }

        public string Nickname
        {
            get => PlayerPrefs.GetString("nickname", "Player1");
            set => PlayerPrefs.SetString("nickname", value);
        }

        public bool IsTrainedCompleted
        {
            get => Convert.ToBoolean(PlayerPrefs.GetInt("trainedcompleted", 0));
            set => PlayerPrefs.SetInt("trainedcompleted", Convert.ToInt32(value));
        }

        public int EndlessRecord
        {
            get => PlayerPrefs.GetInt("endlessrecord", 0);
            set => PlayerPrefs.SetInt("endlessrecord", value);
        }
        
        public int Rank
        {
            get => PlayerPrefs.GetInt("rank", 1);
            set => PlayerPrefs.SetInt("rank", value);
        }
        
        public int Scors
        {
            get => PlayerPrefs.GetInt("scors", 0);
            set => PlayerPrefs.SetInt("scors", value);
        }
        
        public int Cash
        {
            get => PlayerPrefs.GetInt("cash", _configuration.StartCashCount);
            set
            {
                PlayerPrefs.SetInt("cash", value); 
                ChangeCashValue?.Invoke(value);
            }
        }
    }
}