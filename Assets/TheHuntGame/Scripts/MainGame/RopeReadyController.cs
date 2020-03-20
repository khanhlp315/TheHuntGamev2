using UnityEngine;
using System.Collections;

public class RopeReadyController : RopeController
{
    public GameObject rope;
    public GameObject[] arrows;

    public override void  StartCatch(int numberAnimal) {
        switch (numberAnimal) {

            case 1:
                rope.GetComponent<Animator>().Play("rop1");
                arrows[0].SetActive(true);
                arrows[1].SetActive(false);
                arrows[2].SetActive(false);
                arrows[3].SetActive(false);
                arrows[4].SetActive(false);
                break;
            case 2:
                rope.GetComponent<Animator>().Play("rop2");
               
                arrows[0].SetActive(true);
                arrows[1].SetActive(true);
                arrows[2].SetActive(false);
                arrows[3].SetActive(false);
                arrows[4].SetActive(false);
                break;
            case 3:
                rope.GetComponent<Animator>().Play("rop3");
          
                arrows[0].SetActive(true);
                arrows[1].SetActive(true);
                arrows[2].SetActive(true);
                arrows[3].SetActive(false);
                arrows[4].SetActive(false);
                break;
            case 4:
                rope.GetComponent<Animator>().Play("rop4");
            

                arrows[0].SetActive(false);
                arrows[1].SetActive(true);
                arrows[2].SetActive(true);
                arrows[3].SetActive(true);
                arrows[4].SetActive(true);
                break;
            case 5:
                rope.GetComponent<Animator>().Play("rop5");
                arrows[0].SetActive(true);
                arrows[1].SetActive(true);
                arrows[2].SetActive(true);
                arrows[3].SetActive(true);
                arrows[4].SetActive(true);
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
