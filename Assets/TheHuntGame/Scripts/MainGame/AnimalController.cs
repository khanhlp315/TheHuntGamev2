using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Spine.Unity;
using TheHuntGame.GameMachine;
using DG.Tweening;
using TheHuntGame.EventSystem;
using TheHuntGame.EventSystem.Events;

public class AnimalController : MonoBehaviour
{
    [NonSerialized]
    public int Coins = 0;

    [NonSerialized]
    public long Id;

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
    public int ropeIndex;

    [NonSerialized]
    public int MaxPressTug = 0;

    [NonSerialized]
    public float StartCatchPosition = 0;

    [NonSerialized]
    public float EndCatchPosition = 0;

    [SerializeField]
    private MeshRenderer coinRender;

    private int _pressTug = 0;

    private bool _tug = false;

    private Vector3 positionA = Vector3.zero;
    private Vector3 positionB = Vector3.zero;
    private Vector3 positionC = Vector3.zero;

    public void Init()
    {
        coinRender.sortingOrder = 26;
    }
    public void Resist()
    {
        GetComponent<Animator>().Play("resist");
        IsCatch = true;
        _tug = true;
        GetComponent<SkeletonAnimator>().GetComponent<MeshRenderer>().sortingOrder = 40;
        coinRender.sortingOrder = 41;
    }

    public void Caught()
    {
     
        GetComponent<Animator>().Play("catched");
      
        positionA = transform.position;
        positionB = positionA + new Vector3(0, StartCatchPosition, 0);
        positionC = positionA + new Vector3(0, EndCatchPosition, 0);

        Sequence throwSequence = DOTween.Sequence();
        //day chay len lan 1
        
        throwSequence.Append(transform.DOMove(positionB, 1f));
        //day chay len lan 2
        throwSequence.Append(transform.DOMove(positionC , 1f));
        throwSequence.AppendCallback(() =>
        {
            //sau khi nem xong
            

            //EventSystem.EventSystem.Instance.Emit(new RopeTugEvent()
            //{
            //    RopeIndex = _ropeIndex
            //});

        });
        _coinsText.gameObject.SetActive(false);
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
        else if(_tug)
        {
          
            if(transform.position.y == EndTugPosition)
            {
                EventSystem.Instance.Emit(new AnimalCatchEvent() { Id = Id, RopeIndex = ropeIndex });
                _tug = false;

            }
            else if (GameMachine.Instance.GetButtonDown(GameMachineButtonCode.CENTER) || Input.GetKeyDown(KeyCode.Space))
            {
                _pressTug += 1;

                transform.position = Vector3.Lerp(new Vector3(transform.position.x, StartTugPosition, transform.position.z),

                    new Vector3(transform.position.x, EndTugPosition, transform.position.z), _pressTug*1.0f / MaxPressTug);
            }
        }

    }


}
