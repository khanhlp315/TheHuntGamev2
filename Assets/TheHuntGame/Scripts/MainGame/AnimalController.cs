using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Spine.Unity;
using TheHuntGame.GameMachine;

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

    [NonSerialized]
    public float StartTugPosition;

    [NonSerialized]
    public float EndTugPosition;

    [Header("------------Coin---------------")]
    [SerializeField]
    public TextMesh _coinsText;

    [NonSerialized]
    public bool IsCatch = false;

    [NonSerialized]
    public int MaxPressTug = 0;

    [SerializeField]
    private MeshRenderer coinRender;

    private int _pressTug = 0;
  
    public void Init()
    {
        coinRender.sortingOrder = 26;
    }
    public void Resist()
    {
        GetComponent<Animator>().Play("resist");
        IsCatch = true;
        GetComponent<SkeletonAnimator>().GetComponent<MeshRenderer>().sortingOrder = 40;
        coinRender.sortingOrder = 41;
    }
    // Use this for initialization
    void Start()
    {
        coinRender.sortingOrder = 26;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (!IsCatch)
        {
            var position = transform.position;
            position += Vector3.left * (MovementSpeed * Time.deltaTime);
            if (position.x < EndPosition)
            {
                position.x = StartPosition;
            }
            transform.position = position;

        }
        else
        {
            if (GameMachine.Instance.GetButtonDown(GameMachineButtonCode.CENTER) || Input.GetKeyDown(KeyCode.Space))
            {
                _pressTug += 1;

                transform.position = Vector3.Lerp(new Vector3(transform.position.x, StartTugPosition, transform.position.z),

                    new Vector3(transform.position.x, EndTugPosition, transform.position.z), _pressTug*1.0f / MaxPressTug);
            }
        }

    }


}
