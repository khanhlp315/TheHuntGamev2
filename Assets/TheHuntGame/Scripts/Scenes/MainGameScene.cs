using System;
using System.Collections.Generic;
using System.Linq;
using TheHuntGame.EventSystem.Events;
using TheHuntGame.MainGame.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace TheHuntGame.Scenes
{
    public class MainGameScene : MonoBehaviour
    {
        
        [Header("-------------Game Setting------------")]
        [SerializeField]
        private GameSettings _gameSettings;
        
        [Header("------------Character---------------")]
        private List<AnimalController> _animals;
        
        [Header("------------UI---------------")]
        [SerializeField]
        private Text _coinsText;
        public GameObject _logo;

        

        public void FakeInsertCoin()
        {
            EventSystem.EventSystem.Instance.Emit<CoinInsertEvent>();
        }

        private void Start()
        {
            _animals = new List<AnimalController>();
            RegisterEvents();
        }

        public void RegisterEvents()
        {
            EventSystem.EventSystem.Instance.Bind<GameStartEvent>(OnGameStart);
            EventSystem.EventSystem.Instance.Bind<CoinsUpdatedEvent>(OnCoinsUpdated);

        }

        private void OnCoinsUpdated(CoinsUpdatedEvent e)
        {
            _coinsText.text = e.NumberOfCoins.ToString();
        }

        private void OnGameStart(GameStartEvent e)
        {
            _logo.gameObject.SetActive(false);
            var animalsCount = 0;
            foreach (var animalData in e.Animals)
            {
                var characterToInstantiate =
                    _gameSettings.Characters.FirstOrDefault(c => c.CharacterName == animalData.AnimalName);
                Debug.Log(animalData.AnimalName);
                var animal = Instantiate(characterToInstantiate);
                animal.Coins = animalData.AnimalValue;
                animal.StartPosition = _gameSettings.AnimalStartPosition.x;
                animal.EndPosition = _gameSettings.AnimalEndPosition.x;
                animal.MovementSpeed = _gameSettings.AnimalMovementSpeed;

                animal.transform.position = _gameSettings.AnimalStartPosition +
                                            animalsCount * _gameSettings.AnimalOffsetDistance * Vector2.right;
                _animals.Add(animal);

                animalsCount++;
            }
        }
    }
}