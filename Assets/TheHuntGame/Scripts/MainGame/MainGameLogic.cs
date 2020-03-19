using System;
using System.Collections.Generic;
using TheHuntGame.EventSystem.Events;
using TheHuntGame.MainGame.Settings;
using TheHuntGame.Network.Data;
using TheHuntGame.Scenes;
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
        [SerializeField] private GameSettings _gameSetting;

        private GameState _gameState;

        private int _coins = 0;

        private List<AnimalData> _animals;

        private void Awake()
        {
            _gameState = GameState.Waiting;
            EventSystem.EventSystem.Instance.Bind<CoinInsertEvent>(OnCoinInserted);

            EventSystem.EventSystem.Instance.Bind<AnimalCatchEvent>(OnAnimalCatch);
        }

        private void OnAnimalCatch(AnimalCatchEvent e)
        {
            
        }



        private void OnCoinInserted(CoinInsertEvent e)
        {
            _coins++;
            EventSystem.EventSystem.Instance.Emit(new CoinsUpdatedEvent()
            {
                NumberOfCoins = _coins
            });

            if (_gameState == GameState.Waiting)
            {
                StartGame();
            }
        }


      

        private void StartGame()
        {
            _gameState = GameState.Running;
            Network.Network.Instance.CreatePlayer(String.Empty, (playerData) =>
            {
                Network.Network.Instance.CreateGame(playerData.Id, (gameData) =>
                {
                    _animals = gameData.Animals;
                    _animals.Sort((a1, a2) => a1.AnimalOrder.CompareTo(a2.AnimalOrder));
                    EventSystem.EventSystem.Instance.Emit(new GameStartEvent()
                    {
                        Animals = _animals
                    });
                }, (error) =>
                {
                    Debug.LogError(error);
                });
            }, (error) =>
            {
                Debug.LogError(error);
            });
        }
    }
}
