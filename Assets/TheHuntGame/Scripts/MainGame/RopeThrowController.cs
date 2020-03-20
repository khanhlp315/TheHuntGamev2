using UnityEngine;
using System.Collections;

public class RopeThrowController : RopeController
{
    public GameObject[] ropes;
    public override void StartCatch(int numberAnimal)
    {
        switch (numberAnimal)
        {
            case 1:
                ropes[0].SetActive(true);
                ropes[1].SetActive(false);
                ropes[2].SetActive(false);
                ropes[3].SetActive(false);
                ropes[4].SetActive(false);
                break;
            case 2:
                ropes[2].SetActive(true);
                ropes[3].SetActive(true);
                break;
            case 3:
                ropes[1].SetActive(true);
                ropes[2].SetActive(true);
                ropes[3].SetActive(true);
                break;
            case 4:
                ropes[1].SetActive(true);
                ropes[2].SetActive(true);
                ropes[3].SetActive(true);
                ropes[4].SetActive(true);
                break;
            case 5:
                ropes[0].SetActive(true);
                ropes[1].SetActive(true);
                ropes[2].SetActive(true);
                ropes[3].SetActive(true);
                ropes[4].SetActive(true);
                break;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
