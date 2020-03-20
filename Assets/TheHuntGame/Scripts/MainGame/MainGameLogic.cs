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
        Catched,
        End
    }
    public class MainGameLogic : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSetting;

        private GameState _gameState;

        private int _coins = 0;

        private List<AnimalData> _animals;

        private float timeToCatch = 0;
        private void Awake()
        {
            _gameState = GameState.Waiting;
            EventSystem.EventSystem.Instance.Bind<CoinInsertEvent>(OnCoinInserted);
            EventSystem.EventSystem.Instance.Bind<GameStartedEvent>(OnGameStarted);

            
        }

        private void OnGameStarted(GameStartedEvent e)
        {
            _gameState = GameState.Running;

        }

        private void Update()
        {
            switch (_gameState) {
                case GameState.Running:
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        _gameState = GameState.Catching;
                        EventSystem.EventSystem.Instance.Emit<AnimalCatchingEvent>();
                    }

                    break;
                case GameState.Catching:
                    timeToCatch += Time.deltaTime;
                    if (timeToCatch >= _gameSetting.CatchAnimalTime) {
                        _gameState = GameState.Catched;
                        EventSystem.EventSystem.Instance.Emit<AnimalCatchedEvent>();
                    }
            
                    break;
            }
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
            if(_gameState == GameState.Running)
            {
                EventSystem.EventSystem.Instance.Emit<RopeUpdatedEvent>();
            }
        }


      

        private void StartGame()
        {
        
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
