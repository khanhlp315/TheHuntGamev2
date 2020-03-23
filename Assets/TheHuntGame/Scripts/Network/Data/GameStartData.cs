using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheHuntGame.Network.Data
{
    [Serializable]
    public class GameStartData
    {
        [SerializeField]
        private int coinsUsed;

        [SerializeField]
        private List<long> selectedAnimalIds;

        public GameStartData()
        {
            selectedAnimalIds = new List<long>();
        }
        public int CoinsUsed
        {
            get => coinsUsed;
            set => coinsUsed = value;
        }

        public List<long> SelectedAnimalIds
        {
            get => selectedAnimalIds;
            set => selectedAnimalIds = value;
        }
        public static string ToJson(GameStartData gameStartData)
        {
            return JsonUtility.ToJson(gameStartData);
        }

        public static GameStartData FromJson(string jsonString)
        {
            return JsonUtility.FromJson<GameStartData>(jsonString);
        }
    }
}
