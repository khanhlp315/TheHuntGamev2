using System.Collections.Generic;
using UnityEngine;

namespace TheHuntGame.GameMachine
{
    [CreateAssetMenu(fileName = "GameMachineConfig.asset", menuName = "TheHuntGame/Settings/Game Machine Setting", order = 10)]
    public class GameMachineConfig: ScriptableObject
    {
        public int Port = 0;
        public List<GameMachineButton> Buttons;
        public string CoinInsertCommand;
        public string CoinOutCommand;
    }
}