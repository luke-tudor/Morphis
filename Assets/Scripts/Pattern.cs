using UnityEngine;
using System.Collections;

public class Pattern : MonoBehaviour {

    public GameObject[] toExtrude;
    public GameObject[] nonExtrude;
    public GameObject door;
    private bool doorOpened = false;
	// Use this for initialization
	void Start () {
        onExtrude();
    }
	
	// Update is called once per frame
	void Update () {
        onExtrude();
    }

    public bool checkIfSolved()
    {
        foreach (GameObject o in toExtrude)
        {
            if (o.transform.localScale.y == 0.001f)
            {
                return false;
            }
        }

        foreach (GameObject o in nonExtrude)
        {
            if (o.transform.localScale.y == 1f)
            {
                return false;
            }
        }

        return true;
    }

    public void onExtrude()
    {
        if (!doorOpened && checkIfSolved())
        {
            door.SendMessage("setExtrudable", true);
            door.SendMessage("ShrinkCompletely");
            doorOpened = true;
        }
    }
}
