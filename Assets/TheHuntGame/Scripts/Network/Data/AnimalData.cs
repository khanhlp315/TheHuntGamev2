using System;
using UnityEngine;

namespace TheHuntGame.Network.Data
{
    [Serializable]
    public class AnimalData 
    {
        [SerializeField]
        private long id;

        [SerializeField]
        private string animalName;

        [SerializeField]
        private int animalValue;

        [SerializeField]
        private int animalOrder;

        [SerializeField]
        private int result;

        public long Id
        {
            get => id;
            set => id = value;
        }

        public string AnimalName
        {
            get => animalName;
            set => animalName = value;
        }

        public int AnimalValue
        {
            get => animalValue;
            set => animalValue = value;
        }

        public int AnimalOrder
        {
            get => animalOrder;
            set => animalOrder = value;
        }

        public int Result
        {
            get => result;
            set => result = value;
        }
        
        public static AnimalData FromJson(string jsonString)
        {
            return JsonUtility.FromJson<AnimalData>(jsonString);
        }
    }
}