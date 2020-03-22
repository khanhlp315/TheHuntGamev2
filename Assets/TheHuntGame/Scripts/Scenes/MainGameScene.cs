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
            EventSystem.EventSystem.Instance.Bind<RopeThrowEvent>(OnRopeThrow);
            EventSystem.EventSystem.Instance.Bind<RopeTugEvent>(OnRopeTug);


        }

        private void OnRopeTug(RopeTugEvent e)
        {
                    
            int fromValue = 0;
            //   int toValue = 0;
            if (e.RopeIndex % 2 == 0)
            {
                fromValue = e.RopeIndex / 2;
                //  toValue = fromValue + 1;
            }
            else
            {
                fromValue = - (e.RopeIndex / 2 )- 1;
                // toValue = fromValue + 1;

            }
            bool catchAnimal = false;
            for (int i = 0; i < _animals.Count; i++)
            {
                var positionX = _animals[i].transform.position.x;
                if (positionX >= _gameSettings.AnimalOffsetDistance * fromValue
                    && positionX < _gameSettings.AnimalOffsetDistance * (fromValue + 1))
                {
                    if (!_animals[i].IsCatch)
                    {
                        
                        _ropes[e.RopeIndex].HitTug();
                        catchAnimal = true;
                        _animals[i].IsCatch = true;
                        _animals[i].transform.position = new Vector3(_gameSettings.AnimalOffsetDistance * fromValue,
                               _animals[i].transform.position.y, _animals[i].transform.position.z);
                        _animals[i].Resist();
                        break;
                    }

                }
            }
            if (!catchAnimal)
            {
                _ropes[e.RopeIndex].MissedTug();
            }


        }

        private void OnRopeThrow(RopeThrowEvent e)
        {
            _ropePreparation.gameObject.SetActive(false);

            for (int i = 0; i < _arrows.Length; ++i)
            {
                _arrows[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < _ropes.Length; ++i)
            {
                _ropes[i].Throw();
            }

        }

        private void OnRopeUpdated(RopeUpdatedEvent e)
        {
            _ropePreparation.gameObject.SetActive(true);
            _logo.gameObject.SetActive(false);
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

            for (int i = 0; i < _ropes.Length; i++)
            {
                _ropes[i].MaxPressTug = _gameSettings.MaxPressTug;
                _ropes[i].StartTugPosition = _gameSettings.RopeStartTugPosition;
                _ropes[i].EndTugPosition = _gameSettings.RopeEndTugPosition;
            }
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
                animal.StartTugPosition = _gameSettings.AnimalStartTugPosition;
                animal.EndTugPosition = _gameSettings.AnimalEndTugPosition;
                animal.MaxPressTug = _gameSettings.MaxPressTug;
                animal.Init();
                _animals.Add(animal);

                animalsCount++;
            }
            EventSystem.EventSystem.Instance.Emit(new GameStartedEvent());
        }
    }
}