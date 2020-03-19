using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimalController : MonoBehaviour
{
    [NonSerialized]
    public int Coins = 0;
    
    public string CharacterName ;

    [NonSerialized]
    public float MovementSpeed;
    
    [NonSerialized]
    public float StartPosition;
    
    [NonSerialized]
    public float EndPosition;

    [Header("------------Coin---------------")]
    [SerializeField]
    public Text _coinsText;
    // Use this for initialization
    void Start()
    {

    }
    
    
    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        position += Vector3.left * (MovementSpeed * Time.deltaTime);
        if (position.x < EndPosition)
        {
            position.x = StartPosition;
        }
        transform.position = position;
    }


}
