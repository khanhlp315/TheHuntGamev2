using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHuntGame.EventSystem.Events;
using TheHuntGame.Utilities;
using UnityEngine;

namespace TheHuntGame.GameMachine
{
    public class Player : MonoSingleton<Player, GameMachineConfig>
    {
        private int _coins = 0;
        public int Coins
        {
            get => _coins;
            set
            {
                _coins = value;
                PlayerPrefs.SetInt("Coins", _coins);
                PlayerPrefs.Save();
            }
        }

        public override void Initialize()
        {
            _coins = PlayerPrefs.GetInt("Coins", 0);
            EventSystem.EventSystem.Instance.Bind<CoinInsertEvent>(OnCoinInserted);

        }

        private void OnCoinInserted(CoinInsertEvent e)
        {
            Coins += 1;
        }
    }
}
