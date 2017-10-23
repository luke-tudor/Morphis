using UnityEngine;
using System.Collections;

public class CloseDoor : MonoBehaviour {

    bool doorClosed = false;
    public GameObject door;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter(Collider other)
    {
        if (!doorClosed && other.tag == "Player")
        {
            door.SendMessage("setExtrudable", true);
            door.SendMessage("GrowCompletely");
            doorClosed = true;
        }
    }
}
