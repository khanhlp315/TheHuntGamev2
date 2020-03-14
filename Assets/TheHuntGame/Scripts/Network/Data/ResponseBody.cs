using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheHuntGame.Network.Data
{
    [Serializable]
    public class ResponseBodyWithSingleEntity<T>
    {
        [SerializeField]
        private T data;

        public T Get()
        {
            return data;
        }
    }
    
    [Serializable]
    public class ResponseBodyWithMultipleEntities<T>
    {
        [SerializeField]
        private List<T> data;

        public List<T> GetAll()
        {
            return data;
        }
    }

}