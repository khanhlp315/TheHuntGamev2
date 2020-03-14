using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheHuntGame.UI
{
    public class ScrollingBackground : MonoBehaviour
    {
        [SerializeField]
        private float _offset;
        [SerializeField]
        private float _speed;

        public bool IsMoving = true;

        private List<Transform> _scrollingObjects;

        private void Start()
        {
            _scrollingObjects = new List<Transform>();
            var childObject = transform.GetChild(0);
            _scrollingObjects.Add(childObject);
            var childPosition = childObject.position;
            childPosition.x = 0;
            childObject.position = childPosition;
            _scrollingObjects.Add(Instantiate(childObject, new Vector3(-_offset, childPosition.y, childPosition.z), childObject.rotation, transform));
        }


        private void Update()
        {
            if (IsMoving)
            {
                foreach (var obj in _scrollingObjects)
                {
                    var position = obj.transform.position;

                    position.x += _speed * Time.deltaTime;
                    if (position.x > _offset)
                    {
                        position.x = -_offset;
                    }

                    obj.position = position;
                }
            }
        }
    }
}