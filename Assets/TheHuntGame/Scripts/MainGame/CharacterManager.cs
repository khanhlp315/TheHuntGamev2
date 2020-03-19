using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using TheHuntGame.Network.Data;
using System.Linq;
using UnityEngine;

// public class CharacterManager : MonoBehaviour
// {
//     #region ===== Fields =====
//
//     public int      offsetDistance  = 275;
//     public float    movementSpeed   = 400f;
//     public float    escapedSpeed    = 600f;
//     public Vector3  startPoint      = Vector3.zero;
//     public Vector3  endPoint        = Vector3.zero;
//     public List<CharacterController> characters = new List<CharacterController>();
//
//     public CharacterController characterBeCaught1 = null;
//     public CharacterController characterBeCaught2 = null;
//     public CharacterController characterBeCaught3 = null;
//     public CharacterController characterBeCaught4 = null;
//     public CharacterController characterBeCaught5 = null;
//
//     public bool caughted1 = false;
//     public bool caughted2 = false;
//     public bool caughted3 = false;
//     public bool caughted4 = false;
//     public bool caughted5 = false;
//
//     #endregion
//
//     #region ===== Properties =====
//
//     #endregion
//
//     #region ===== Main Methods =====
//
//     // Start is called before the first frame update
//     void Start()
//     {
//         Reset();
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//     }
//
//     #endregion
//
//     #region ===== Methods =====
//
//     public void Reset()
//     {
//        // characters.Sort((a, b) => b.ID.CompareTo(a.ID));
//         //for (int i = 0; i < 20; ++i)
//         //{
//         //    characters.Sort((a, b) => UnityEngine.Random.Range(-1, 1));
//         //}
//         //for (int i = 0; i < characters.Count; i++)
//         //{
//         //    var character   = characters[i];
//         //    var position    = startPoint + i * offsetDistance * Vector3.right;
//         //    character.transform.position = position;
//         //}
//
//     }
//
//     public void Sort(AnimalData[] animals)
//     {
//
//         List<CharacterController> oldCharacters = new List<CharacterController>();
//
//         oldCharacters.AddRange(characters);
//
//         animals = animals.OrderBy(animal => animal.AnimalOrder).ToArray();
//
//         characters.Clear();
//
//         for (int i = 0; i < animals.Length; i++)
//         {
//             CharacterController findCharacter = oldCharacters.Find((character) => character.Name == animals[i].AnimalName);
//
//             findCharacter = Instantiate(findCharacter);
//             characters.Add(findCharacter);
//
//
//         }
//
//         for (int i = 0; i < characters.Count; i++)
//         {
//             var character = characters[i];
//             var position = startPoint + i * offsetDistance * Vector3.right;
//             character.Init(this);
//             character.transform.position = position;
//         }
//     }
//
//     public void BattleOnIdle()
//     {
//         Reset();
//     }
//
//     public void BattleOnReady()
//     {
//         foreach (var character in characters)
//         {
//            
//         }
//     }
//
//     public void BattleOnCatch()
//     {
//         
//     }
//
//     public void BattleOnTug()
//     {
//        
//     }
//
//     public void BattleOnResult()
//     {
//         
//     }
//
//     #endregion
// }
