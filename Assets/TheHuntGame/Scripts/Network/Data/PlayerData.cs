using System;
using UnityEngine;

namespace TheHuntGame.Network.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField]
        private long id;

        [SerializeField]
        private string playerName;


        public long Id
        {
            get => id;
            set => id = value;
        }

        public string PlayerName
        {
            get => playerName;
            set => playerName = value;
        }

        public static PlayerData FromJson(string jsonString)
        {
            return JsonUtility.FromJson<PlayerData>(jsonString);
        }
        
    }
}