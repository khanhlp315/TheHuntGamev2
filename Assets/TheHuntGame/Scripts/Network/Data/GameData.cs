using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheHuntGame.Network.Data
{
    [Serializable]
    public class GameData
    {
        [SerializeField]
        private long id;

        [SerializeField]
        private long gameMachineId;

        [SerializeField]
        private long playerId;

        [SerializeField]
        private string createTime;

        [SerializeField]
        private string startTime;

        [SerializeField]
        private string endTime;

        [SerializeField]
        private int coinsUsed;

        [SerializeField]
        private int coinsEarned;

        [SerializeField]
        private List<AnimalData> animals;

        [SerializeField]
        private List<AnimalData> selectedAnimals;

        public long Id
        {
            get => id;
            set => id = value;
        }

        public long GameMachineId
        {
            get => gameMachineId;
            set => gameMachineId = value;
        }

        public long PlayerId
        {
            get => playerId;
            set => playerId = value;
        }

        public string CreateTime
        {
            get => createTime;
            set => createTime = value;
        }

        public string StartTime
        {
            get => startTime;
            set => startTime = value;
        }

        public string EndTime
        {
            get => endTime;
            set => endTime = value;
        }

        public int CoinsUsed
        {
            get => coinsUsed;
            set => coinsUsed = value;
        }

        public int CoinsEarned
        {
            get => coinsEarned;
            set => coinsEarned = value;
        }

        public List<AnimalData> Animals
        {
            get => animals;
            set => animals = value;
        }

        public List<AnimalData> SelectedAnimals
        {
            get => selectedAnimals;
            set => selectedAnimals = value;
        }
        
        public static GameData FromJson(string jsonString)
        {
            return JsonUtility.FromJson<GameData>(jsonString);
        }
    }
}
