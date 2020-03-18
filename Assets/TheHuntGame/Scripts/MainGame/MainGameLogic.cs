using System;
using TheHuntGame.EventSystem.Events;
using UnityEngine;

namespace TheHuntGame.MainGame
{
    enum GameState
    {
        Waiting,
        Running,
        Catching,
        End
    }
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
