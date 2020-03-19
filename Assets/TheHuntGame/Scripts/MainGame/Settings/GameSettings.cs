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

        [Header("Animals")]
        public List<AnimalController> Characters;
    }
}