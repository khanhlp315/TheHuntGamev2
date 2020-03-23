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
        public int MaxRopes;
        public float AnimalStartTugPosition = 50;
        public float AnimalEndTugPosition = -200;
        public float RopeStartTugPosition = -250;
        public float RopeEndTugPosition = -500;
        public float StartCatchPosition = 800;
        public float EndCatchPosition = -100;

        public int MaxPressTug = 20;

        [Header("Animals")]
        public List<AnimalController> Characters;
    }
}