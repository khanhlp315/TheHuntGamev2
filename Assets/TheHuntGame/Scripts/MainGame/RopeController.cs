using System;
using TheHuntGame.EventSystem.Events;
using UnityEngine;

namespace TheHuntGame.MainGame
{
    public class RopeController : MonoBehaviour
    {
        [SerializeField] private int _ropeIndex;
        [SerializeField] private Transform _throwObject;
        [SerializeField] private Transform _tugObject;
        [SerializeField] private Transform _caughtObject;

        private bool _isUsed = false;

        private void Awake()
        {
            EventSystem.EventSystem.Instance.Bind<RopeUpdatedEvent>(OnRopeUpdated);
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
