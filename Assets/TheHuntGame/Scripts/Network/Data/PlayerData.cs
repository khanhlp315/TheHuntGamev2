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
    }
    
    [Serializable]
    public class PlayerDataResponse : ResponseBodyWithSingleEntity<PlayerData>
    {
        public static PlayerDataResponse FromJson(string jsonString)
        {
            return JsonUtility.FromJson<PlayerDataResponse>(jsonString);
        }
    }

}