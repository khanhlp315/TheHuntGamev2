using System;
using System.Collections.Generic;
using System.Linq;
using TheHuntGame.EventSystem.Events;
using TheHuntGame.MainGame;
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
        [SerializeField]
        private GameObject _logo;

        [Header("------------Rope---------------")]
        [SerializeField]
        private Animator _ropePreparation;
        [SerializeField]
        private GameObject[] _arrows;
        [SerializeField]
        private RopeController[] _ropes;


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
            EventSystem.EventSystem.Instance.Bind<RopeUpdatedEvent>(OnRopeUpdated);
            EventSystem.EventSystem.Instance.Bind<CoinsUpdatedEvent>(OnCoinsUpdated);

        }

        private void OnRopeUpdated(RopeUpdatedEvent e)
        {
            _ropePreparation.Play($"{e.NumberOfRopes}");
            for (int i = 0; i < e.NumberOfRopes; ++i)
            {
                _arrows[i].gameObject.SetActive(true);
            }
        }

        private void OnCoinsUpdated(CoinsUpdatedEvent e)
        {
            _coinsText.text = e.NumberOfCoins.ToString();
        }

        private void OnGameStart(GameStartEvent e)
        {
            _ropePreparation.gameObject.SetActive(true);
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
                animal._coinsText.text = animalData.AnimalValue.ToString();
                animal.transform.position = _gameSettings.AnimalStartPosition +
                                            animalsCount * _gameSettings.AnimalOffsetDistance * Vector2.right;
                _animals.Add(animal);

                animalsCount++;
            }
        }
    }
}