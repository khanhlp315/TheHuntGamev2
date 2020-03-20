using Spine.Unity;
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

        public RopeController[] _ropes;

        private int coins;
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
            EventSystem.EventSystem.Instance.Bind<AnimalCatchingEvent>(OnAnimalCatching);
            EventSystem.EventSystem.Instance.Bind<AnimalCatchedEvent>(OnAnimalCatched);

        }
        private void OnAnimalCatched(AnimalCatchedEvent e) {
      
            foreach (var animal in _animals)
            {

                if (coins == 1)
                {
                    var positionX = animal.transform.position.x;
                    if (positionX >= _gameSettings.AnimalOffsetDistance * 0 && positionX < _gameSettings.AnimalOffsetDistance * 1)
                    {
                        _ropes[1].gameObject.SetActive(false);
                        _ropes[2].gameObject.SetActive(true);
                        _ropes[2].StartCatch(coins);
                        animal.Catch();
                        animal.transform.position = new Vector3(0, animal.transform.position.y, animal.transform.position.z);
                    }
                }

            }
        }
        private void OnAnimalCatching(AnimalCatchingEvent e)
        {
            _ropes[0].gameObject.SetActive(false);
            _ropes[1].gameObject.SetActive(true);
            _ropes[1].StartCatch(coins);

        }
        private void OnCoinsUpdated(CoinsUpdatedEvent e)
        {
            coins = e.NumberOfCoins;
            _coinsText.text = e.NumberOfCoins.ToString();
        }

        private void OnGameStart(GameStartEvent e)
        {
            _logo.gameObject.SetActive(false);  
            var animalsCount = 0;
            _ropes[0].gameObject.SetActive(true);
            _ropes[0].StartCatch(coins);
            //  _ropes[0].GetComponentsInChildren(. GetComponent<Animator>().Play("rop" + coins.ToString());
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
                animal.AnimalStartTugPosition = _gameSettings.AnimalStartTugPoint;
                animal.AnimalEndTugPosition = _gameSettings.AnimalEndTugPoint;
                animal.TugSpeed = _gameSettings.AnimalTugSpeed;
                animal.PressButtonTimes = _gameSettings.PressButtonTimes;
                _animals.Add(animal);

                animalsCount++;
            }
        }
    }
}