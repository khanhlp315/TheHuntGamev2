using System.Collections.Generic;
using UnityEngine;

namespace TheHuntGame.MainGame.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "TheHuntGame/Settings/Game Settings", order = 10)]
    public class GameSettings : ScriptableObject
    {
        [Header("General")]
		public Vector2 AnimalStartPosition;
        public Vector2 AnimalEndPosition;
        public float AnimalOffsetDistance;
        public float AnimalMovementSpeed;
        public float AnimalEscapeSpeed;
        public float AnimalTugSpeed;
        public float CatchAnimalTime;
        public int PressButtonTimes;
 
        public Vector3 AnimalStartTugPoint = new Vector3(0f, 50, 0f);
        public Vector3 AnimalEndTugPoint = new Vector3(0f, -200, 0f);


        [Header("Animals")]
        public List<AnimalController> Characters;
    }
}