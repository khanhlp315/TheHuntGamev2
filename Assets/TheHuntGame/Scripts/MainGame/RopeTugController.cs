using UnityEngine;
using System.Collections;

public class RopeTugController : RopeController
{
    public RopeTug[] ropeTugs;
    public override void StartCatch(int numberAnimal)
    {
        switch (numberAnimal)
        {
            case 1:
                ropeTugs[0].gameObject.SetActive(true);
                ropeTugs[1].gameObject.SetActive(false);
                ropeTugs[2].gameObject.SetActive(false);
                ropeTugs[3].gameObject.SetActive(false);
                ropeTugs[4].gameObject.SetActive(false);
                break;
            case 2:
                ropeTugs[2].gameObject.SetActive(true);
                ropeTugs[3].gameObject.SetActive(true);
                break;
            case 3:
                ropeTugs[1].gameObject.SetActive(true);
                ropeTugs[2].gameObject.SetActive(true);
                ropeTugs[3].gameObject.SetActive(true);
                break;
            case 4:
                ropeTugs[1].gameObject.SetActive(true);
                ropeTugs[2].gameObject.SetActive(true);
                ropeTugs[3].gameObject.SetActive(true);
                ropeTugs[4].gameObject.SetActive(true);
                break;
            case 5:
                ropeTugs[0].gameObject.SetActive(true);
                ropeTugs[1].gameObject.SetActive(true);
                ropeTugs[2].gameObject.SetActive(true);
                ropeTugs[3].gameObject.SetActive(true);
                ropeTugs[4].gameObject.SetActive(true);
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
