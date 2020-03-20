using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TheHuntGame.MainGame;
using Spine.Unity;
namespace TheHuntGame.MainGame
{
    public class AnimalController : MonoBehaviour
    {
        enum AnimalState
        {
            Running,
            Catching,
            Escaped,
            End
        }

        [NonSerialized]
        public int Coins = 0;

        public string CharacterName;

        [NonSerialized]
        public float MovementSpeed;

        [NonSerialized]
        public float StartPosition;

        [NonSerialized]
        public float EndPosition;

        [NonSerialized]
        public float TugSpeed;

        [NonSerialized]
        public Vector3 AnimalStartTugPosition;

        [NonSerialized]
        public Vector3 AnimalEndTugPosition;

        [NonSerialized]
        public int PressButtonTimes;

        private int currentPressTimes = 0;

        [NonSerialized]
        private AnimalState animalState;

        [Header("------------Coin---------------")]
        [SerializeField]
        public Text _coinsText;


        // Use this for initialization
        void Start()
        {

        }

        public void Catch()
        {
            animalState = AnimalState.Catching;
            GetComponentInChildren<SkeletonAnimator>().GetComponent<MeshRenderer>().sortingOrder = 40;
            GetComponentInChildren<Canvas>().sortingOrder = 40;
            GetComponent<Animator>().Play("resist");
        }

        public void Escaped()
        {
            animalState = AnimalState.Escaped;
            GetComponent<Animator>().Play("escaped");
        }

        // Update is called once per frame
        void Update()
        {
            switch (animalState)
            {
                case AnimalState.Running:
                    var position = transform.position;
                    position += Vector3.left * (MovementSpeed * Time.deltaTime);
                    if (position.x < EndPosition)
                    {
                        position.x = StartPosition;
                    }
                    transform.position = position;
                    break;
                case AnimalState.Catching:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        var progress = currentPressTimes / PressButtonTimes;

                        transform.position = Vector3.Lerp(AnimalStartTugPosition, AnimalEndTugPosition, progress);

                    }
                    break;
            }

        }


    }
}