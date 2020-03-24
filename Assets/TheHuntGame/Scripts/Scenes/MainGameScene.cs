using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using TheHuntGame.EventSystem.Events;
using TheHuntGame.GameMachine;
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
        private Text _resultCoinsText;

        [SerializeField]
        private GameObject _logo;

        [SerializeField]
        private GameObject _winPopup;

        [Header("------------Rope---------------")]

        [SerializeField]
        private Animator _ropePreparation;

        [SerializeField]
        private GameObject[] _arrows;

        [SerializeField]
        private RopeController[] _ropes;

        [SerializeField]
        private GameObject[] _brokenRopes;

        [SerializeField]
        private GameObject _hand;

        [SerializeField]
        private GameObject _knot;

        private bool _tug;

        private int _pressTug;

        private int _earnCoins;

        private bool _gameEnd;
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
            EventSystem.EventSystem.Instance.Bind<GameResultEvent>(OnGameResult);
            EventSystem.EventSystem.Instance.Bind<GameCaughtEvent>(OnGameCaught);
     


        }

        private void OnGameCaught(GameCaughtEvent e)
        {
            var escapeNumber = 0;
            _knot.SetActive(false);
           
            foreach (var selectedAnimal in e.SelectedAnimals)
            {
                //catched animal
                if (selectedAnimal.Result == 1)
                {
                    var catchedAnimal = _animals.FirstOrDefault((animal) => animal.Id == selectedAnimal.Id);
                    if (catchedAnimal.IsCatch)
                    {
                        catchedAnimal.Caught();
                        _ropes[catchedAnimal.ropeIndex].Caught();
                        _earnCoins += selectedAnimal.AnimalValue;
                    }
                }
                else
                {
                    var escapeAnimal = _animals.FirstOrDefault((animal) => animal.Id == selectedAnimal.Id);
                    _ropes[escapeAnimal.ropeIndex].Break();
                    escapeAnimal.Escape();
                    escapeNumber++;
                }
            }
            for (int i = 0; i < _brokenRopes.Length; i++)
            {
                if (i == escapeNumber - 1)
                {
                    _brokenRopes[escapeNumber - 1].SetActive(true);
                }
                else
                {
                    _brokenRopes[i].SetActive(false);
                }

            }


        }

        private void OnGameResult(GameResultEvent e)
        {
            _winPopup.SetActive(true);
            _resultCoinsText.text = _earnCoins.ToString();

            if (!_gameEnd)
            {
                _gameEnd = true;
                Sequence throwSequence = DOTween.Sequence();

                throwSequence.AppendInterval(5f);

                throwSequence.AppendCallback(() =>
                {
                    EventSystem.EventSystem.Instance.Emit(new GameEndEvent());
                });
            }


        }

        private void Update()
        {
            if (_tug)
            {
                if (_knot.transform.position.y == _gameSettings.RopeEndTugPosition)
                {
                    _tug = false;

                }
                else if (GameMachine.GameMachine.Instance.GetButtonDown(GameMachineButtonCode.CENTER) || Input.GetKeyDown(KeyCode.Space))
                {
                    _pressTug += 1;
                    Vector3 newPos = Vector3.Lerp(new Vector3(_knot.transform.position.x,
                        _gameSettings.RopeStartTugPosition, _knot.transform.position.z),

                        new Vector3(_knot.transform.position.x, _gameSettings.RopeEndTugPosition,
                        _knot.transform.position.z), _pressTug * 1.0f / _gameSettings.MaxPressTug);

                    _knot.transform.position = newPos;
                }
            }
        }
        private void OnRopeTug(RopeTugEvent e)
        {
            _hand.SetActive(true);
            _knot.SetActive(true);
            _tug = true;
            int fromValue = 0;
            if (e.RopeIndex % 2 == 0)
            {
                fromValue = e.RopeIndex / 2;
            }
            else
            {
                fromValue = -(e.RopeIndex / 2) - 1;

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
                        _animals[i].ropeIndex = e.RopeIndex;
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
                _ropes[i].StartCatchPosition = _gameSettings.StartCatchPosition;
                _ropes[i].EndCatchPosition = _gameSettings.EndCatchPosition;
            }
            foreach (var animalData in e.Animals)
            {
                var characterToInstantiate =
                    _gameSettings.Characters.FirstOrDefault(c => c.CharacterName == animalData.AnimalName);

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
                animal.Id = animalData.Id;
                animal.StartCatchPosition = _gameSettings.StartCatchPosition;
                animal.EndCatchPosition = _gameSettings.EndCatchPosition;
                animal.EscapePosition = _gameSettings.EscapePosition;
                animal.Init();
                _animals.Add(animal);

                animalsCount++;
            }
            EventSystem.EventSystem.Instance.Emit(new GameStartedEvent());
        }
    }
}