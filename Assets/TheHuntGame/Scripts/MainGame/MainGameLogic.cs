﻿using System;
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
        Starting,
        Running,
        Catching,
        End
    }
    public class MainGameLogic : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSetting;

        private GameState _gameState;

        private int _coins = 0;

        private int _ropes = 0;

        private List<AnimalData> _animals;

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
            else 
            {
                if (_ropes < _gameSetting.MaxRopes)
                {
                    _ropes++;
                    EventSystem.EventSystem.Instance.Emit(new RopeUpdatedEvent
                    {
                        NumberOfRopes = _ropes
                    });
                }
            }
        }


        private void Update()
        {
            switch (_gameState)
            {
                case GameState.Running:               
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        EventSystem.EventSystem.Instance.Emit(new RopeThrowEvent());
                        _gameState = GameState.Catching;
                    }
                    break;
                
            }
        }

        private void StartGame()
        {
            _gameState = GameState.Starting;
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
                    _ropes++;
                    EventSystem.EventSystem.Instance.Emit(new RopeUpdatedEvent
                    {
                        NumberOfRopes = _ropes
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
