using System;
using TheHuntGame.EventSystem.Events;
using UnityEngine;

namespace TheHuntGame.MainGame
{
    public class MainGameLogic : MonoBehaviour
    {
        private void Awake()
        {
            EventSystem.EventSystem.Instance.Bind<CoinInsertEvent>(OnCoinInserted);
        }

        private void OnCoinInserted(CoinInsertEvent e)
        {
        }
    }
}
