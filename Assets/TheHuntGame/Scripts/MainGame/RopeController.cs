using System;
using TheHuntGame.EventSystem.Events;
using UnityEngine;
using DG.Tweening;
using TheHuntGame.GameMachine;

namespace TheHuntGame.MainGame
{
    public class RopeController : MonoBehaviour
    {
        [SerializeField] private int _ropeIndex;
        [SerializeField] private Transform _throwObject;
        [SerializeField] private Transform _tugObject;
        [SerializeField] private Transform _caughtObject;

        [NonSerialized]
        public float StartTugPosition;

        [NonSerialized]
        public float EndTugPosition;


        [NonSerialized]
        public bool _tug;

        [NonSerialized]
        public int MaxPressTug = 0;

        private int _pressTug = 0;

        private bool _isUsed = false;

        private void Awake()
        {
            EventSystem.EventSystem.Instance.Bind<RopeUpdatedEvent>(OnRopeUpdated);
        }

        public void HitTug()
        {
            _throwObject.gameObject.SetActive(false);
            _tugObject.gameObject.SetActive(true);
            _tug = true;


        }

        public void MissedTug()
        {
            _tugObject.gameObject.SetActive(false);
            _tug = false;

        }

        public void Throw()
        {
            if (_isUsed)
            {
                _throwObject.gameObject.SetActive(true);
                Sequence throwSequence = DOTween.Sequence();
                //day chay len lan 1
                throwSequence.Append(_throwObject.transform.DOMoveY(_throwObject.position.y + 100, 0.5f));
                //day chay len lan 2
                throwSequence.Append(_throwObject.transform.DOMoveY(_throwObject.position.y + 375, 0.5f));
                throwSequence.AppendCallback(() =>
                {
                    //sau khi nem xong
                     _throwObject.gameObject.SetActive(false);
                    EventSystem.EventSystem.Instance.Emit(new RopeTugEvent()
                    {
                        RopeIndex = _ropeIndex
                    });
                   
                });
            }
        }

        private void Update()
        {
            if (_tug)
            {
                if (GameMachine.GameMachine.Instance.GetButtonDown(GameMachineButtonCode.CENTER) || Input.GetKeyDown(KeyCode.Space))
                {
                    _pressTug += 1;

                    _tugObject.transform.position = Vector3.Lerp(new Vector3(_tugObject.transform.position.x, StartTugPosition,
                         _tugObject.transform.position.z),

                    new Vector3(_tugObject.transform.position.x, EndTugPosition, _tugObject.transform.position.z), _pressTug * 1.0f / MaxPressTug);
                }
            }
        }

        private void OnRopeUpdated(RopeUpdatedEvent e)
        {
            if (e.NumberOfRopes > _ropeIndex)
            {
                _isUsed = true;
            }
        }
    }
}
