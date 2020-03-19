using UnityEngine;

namespace TheHuntGame.Network
{
    [CreateAssetMenu(fileName = "NetworkConfig.asset", menuName = "TheHuntGame/Settings/Network Setting", order = 10)]
    public class NetworkConfig: ScriptableObject
    {
        public string BaseUrl = "https://game-machines.herokuapp.com";
        public string CreatePlayerPath = "players";
        public string CreateGamePath = "games";
    }
}