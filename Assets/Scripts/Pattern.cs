using UnityEngine;
using System.Collections;

public class Pattern : MonoBehaviour {

    public GameObject[] toExtrude;
    public GameObject[] nonExtrude;
    public GameObject door;
	// Use this for initialization
	void Start () {
        Debug.Log("what1");

        onExtrude();
        Debug.Log("what2");
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

        Debug.Log("WAAAAT");
        return true;
    }

    public void onExtrude()
    {
        Debug.Log("hello2");
       
        Debug.Log("hello3");
        if (checkIfSolved())
        {
            door.SendMessage("setExtrudable", true);
            door.SendMessage("Shrink");
        }
    }
}
