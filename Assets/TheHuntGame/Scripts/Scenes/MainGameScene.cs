using Spine.Unity;
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
    [Serializable]
    public class RopeReady
    {
        public GameObject root = null;
        public Animator animator = null;
        public GameObject arrow1 = null;
        public GameObject arrow2 = null;
        public GameObject arrow3 = null;
        public GameObject arrow4 = null;
        public GameObject arrow5 = null;
    }

    [Serializable]
    public class RopeThrow
    {
        public GameObject root = null;
        public Animator animator = null;
        public GameObject rope1 = null;
        public GameObject rope2 = null;
        public GameObject rope3 = null;
        public GameObject rope4 = null;
        public GameObject rope5 = null;
    }

    [Serializable]
    public class RopeTug
    {
        public GameObject root = null;
        //public Animator       animator    = null;
        public RopeController rope1 = null;
        public RopeController rope2 = null;
        public RopeController rope3 = null;
        public RopeController rope4 = null;
        public RopeController rope5 = null;
        public GameObject knot = null;
    }

    [Serializable]
    public class RopeCaught
    {
        public GameObject root = null;
        //public Animator animator  = null;
        public GameObject rope1 = null;
        public GameObject rope2 = null;
        public GameObject rope3 = null;
        public GameObject rope4 = null;
        public GameObject rope5 = null;

        public GameObject ropeEnd1 = null;
        public GameObject ropeEnd2 = null;
        public GameObject ropeEnd3 = null;
        public GameObject ropeEnd4 = null;
        public GameObject ropeEnd5 = null;
    }

    [Serializable]
    public class RopeResult
    {
        public GameObject root = null;
        //public Animator animator      = null;
        public GameObject rope1 = null;
        public GameObject rope2 = null;
        public GameObject rope3 = null;
        public GameObject rope4 = null;
        public GameObject rope5 = null;
    }

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

        [Header("------------Rope---------------")]
        public RopeReady ropeReady = null;
        public RopeThrow ropeThrow = null;
        public RopeTug ropeTug = null;
        public RopeCaught ropeCaught = null;
        public RopeResult ropeResult = null;


        [Header("------------Logo---------------")]
        public GameObject _logo;


        private int coins;

        private int numberCatch;

        private bool[] catched;

        public void FakeInsertCoin()
        {
            EventSystem.EventSystem.Instance.Emit<CoinInsertEvent>();
        }

        private void Start()
        {
            catched = new bool[5];
            _animals = new List<AnimalController>();
            RegisterEvents();
        }

        public void RegisterEvents()
        {
            EventSystem.EventSystem.Instance.Bind<GameStartEvent>(OnGameStart);
            EventSystem.EventSystem.Instance.Bind<CoinsUpdatedEvent>(OnCoinsUpdated);
            EventSystem.EventSystem.Instance.Bind<AnimalCatchingEvent>(OnAnimalCatching);
            EventSystem.EventSystem.Instance.Bind<AnimalCatchedEvent>(OnAnimalCatched);
            EventSystem.EventSystem.Instance.Bind<RopeUpdatedEvent>(OnRopeUpdate);

        }

        private void OnRopeUpdate(RopeUpdatedEvent e)
        {
            ropeCaught.root.SetActive(false);
            ropeReady.root.SetActive(true);
            ropeResult.root.SetActive(false);
            ropeThrow.root.SetActive(false);
            ropeTug.root.SetActive(false);


            ropeReady.arrow1.SetActive(true);
            ropeReady.arrow2.SetActive(true);
            ropeReady.arrow3.SetActive(true);
            ropeReady.arrow4.SetActive(true);
            ropeReady.arrow5.SetActive(true);

            ropeReady.arrow1.GetComponent<MeshRenderer>().enabled = coins >= 1;
            ropeReady.arrow2.GetComponent<MeshRenderer>().enabled = coins >= 2;
            ropeReady.arrow3.GetComponent<MeshRenderer>().enabled = coins >= 3;
            ropeReady.arrow4.GetComponent<MeshRenderer>().enabled = coins >= 4;
            ropeReady.arrow5.GetComponent<MeshRenderer>().enabled = coins >= 5;

            if (coins == 1)
            {
                ropeReady.animator.Play("rop1");
            }
            else if (coins == 2)
            {
                ropeReady.animator.Play("rop2");
            }
            else if (coins == 3)
            {
                ropeReady.animator.Play("rop3");
            }
            else if (coins == 4)
            {
                ropeReady.animator.Play("rop4");
            }
            else
            {
                ropeReady.animator.Play("rop5");
            }
        }
        private void OnAnimalCatched(AnimalCatchedEvent e)
        {


            ropeCaught.root.SetActive(false);
            ropeReady.root.SetActive(false);
            ropeResult.root.SetActive(false);
            ropeThrow.root.SetActive(false);
            ropeTug.root.SetActive(true);
            float positionX = 0;

            foreach (var animal in _animals)
            {
                if (coins >= 1 && !catched[0])
                {
                    positionX = animal.transform.position.x;
                    if (positionX >= _gameSettings.AnimalOffsetDistance * 0 &&
                        positionX < _gameSettings.AnimalOffsetDistance * 1)
                    {
                        catched[0] = true;
                        numberCatch++;
                        animal.Catch();
                        animal.transform.position = new Vector3(0, animal.transform.position.y,
                            animal.transform.position.z);
                        continue;
                    }
                   
                }
                if (coins >= 2 && !catched[1])
                {
                    positionX = animal.transform.position.x;
                    if (positionX >= _gameSettings.AnimalOffsetDistance * -1 && positionX < _gameSettings.AnimalOffsetDistance * 0)
                    {
                        numberCatch++;
                        animal.Catch();
                        animal.transform.position = new Vector3(_gameSettings.AnimalOffsetDistance * -1, animal.transform.position.y,
                            animal.transform.position.z);
                        continue;
                    }
                }
                if (coins >= 3 && !catched[2])
                {
                    positionX = animal.transform.position.x;
                    if (positionX >= _gameSettings.AnimalOffsetDistance * 1 && positionX < _gameSettings.AnimalOffsetDistance * 2)
                    {
                        numberCatch++;
                        animal.Catch();
                        animal.transform.position = new Vector3(_gameSettings.AnimalOffsetDistance * 1, animal.transform.position.y,
                            animal.transform.position.z);
                        continue;
                    }
                   
                }
                if (coins >= 4 && !catched[3])
                {
                    positionX = animal.transform.position.x;
                    if (positionX >= _gameSettings.AnimalOffsetDistance * -2 && positionX < _gameSettings.AnimalOffsetDistance * -1)
                    {
                        numberCatch++;
                        animal.Catch();
                        animal.transform.position = new Vector3(_gameSettings.AnimalOffsetDistance * -2, animal.transform.position.y,
                            animal.transform.position.z);
                        continue;
                    }

                }
                if (coins >= 5 && !catched[4])
                {
                    positionX = animal.transform.position.x;
                    if (positionX >= _gameSettings.AnimalOffsetDistance * 2 && positionX < _gameSettings.AnimalOffsetDistance * 3)
                    {
                        numberCatch++;
                        animal.Catch();
                        animal.transform.position = new Vector3(_gameSettings.AnimalOffsetDistance * 2, animal.transform.position.y,
                            animal.transform.position.z);
                        continue;
                    }
                }

            }
            ropeTug.rope1.gameObject.SetActive(numberCatch >= 1);
            ropeTug.rope2.gameObject.SetActive(numberCatch >= 2);
            ropeTug.rope3.gameObject.SetActive(numberCatch >= 3);
            ropeTug.rope4.gameObject.SetActive(numberCatch >= 4);
            ropeTug.rope5.gameObject.SetActive(numberCatch >= 5);

            ropeTug.knot.gameObject.SetActive(numberCatch >= 1);

            ropeTug.rope1.b1.SetActive(true);
            ropeTug.rope1.b2.SetActive(false);
            ropeTug.rope1.b3.SetActive(false);

            ropeTug.rope2.b1.SetActive(true);
            ropeTug.rope2.b2.SetActive(false);
            ropeTug.rope2.b3.SetActive(false);

            ropeTug.rope3.b1.SetActive(true);
            ropeTug.rope3.b2.SetActive(false);
            ropeTug.rope3.b3.SetActive(false);

            ropeTug.rope4.b1.SetActive(true);
            ropeTug.rope4.b2.SetActive(false);
            ropeTug.rope4.b3.SetActive(false);

            ropeTug.rope5.b1.SetActive(true);
            ropeTug.rope5.b2.SetActive(false);
            ropeTug.rope5.b3.SetActive(false);



        }
        private void OnAnimalCatching(AnimalCatchingEvent e)
        {
            ropeCaught.root.SetActive(false);
            ropeReady.root.SetActive(false);
            ropeResult.root.SetActive(false);
            ropeThrow.root.SetActive(true);
            ropeTug.root.SetActive(false);

            ropeThrow.rope1.gameObject.SetActive(coins >= 1);
            ropeThrow.rope2.gameObject.SetActive(coins >= 2);
            ropeThrow.rope3.gameObject.SetActive(coins >= 3);
            ropeThrow.rope4.gameObject.SetActive(coins >= 4);
            ropeThrow.rope5.gameObject.SetActive(coins >= 5);

            ropeThrow.animator.Play("Catch");

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

            ropeCaught.root.SetActive(false);
            ropeReady.root.SetActive(true);
            ropeResult.root.SetActive(false);
            ropeThrow.root.SetActive(false);
            ropeTug.root.SetActive(false);


            ropeReady.arrow1.SetActive(true);
            ropeReady.arrow2.SetActive(true);
            ropeReady.arrow3.SetActive(true);
            ropeReady.arrow4.SetActive(true);
            ropeReady.arrow5.SetActive(true);

            ropeReady.arrow1.GetComponent<MeshRenderer>().enabled = coins >= 1;
            ropeReady.arrow2.GetComponent<MeshRenderer>().enabled = coins >= 2;
            ropeReady.arrow3.GetComponent<MeshRenderer>().enabled = coins >= 3;
            ropeReady.arrow4.GetComponent<MeshRenderer>().enabled = coins >= 4;
            ropeReady.arrow5.GetComponent<MeshRenderer>().enabled = coins >= 5;

            if (coins == 1)
            {
                ropeReady.animator.Play("rop1");
            }
            else if (coins == 2)
            {
                ropeReady.animator.Play("rop2");
            }
            else if (coins == 3)
            {
                ropeReady.animator.Play("rop3");
            }
            else if (coins == 4)
            {
                ropeReady.animator.Play("rop4");
            }
            else
            {
                ropeReady.animator.Play("rop5");
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
                animal.AnimalStartTugPosition = _gameSettings.AnimalStartTugPoint;
                animal.AnimalEndTugPosition = _gameSettings.AnimalEndTugPoint;
                animal.TugSpeed = _gameSettings.AnimalTugSpeed;
                animal.PressButtonTimes = _gameSettings.PressButtonTimes;
                _animals.Add(animal);

                animalsCount++;
            }
            EventSystem.EventSystem.Instance.Emit<GameStartedEvent>();
        }
    }
}