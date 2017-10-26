using UnityEngine;
using System.Collections;


/// <summary>
/// This class handles closing of a door once the player walks through it
/// </summary>
public class CloseDoor : MonoBehaviour {

    bool doorClosed = false;
    public GameObject door;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    }

	/// <summary>
	/// Raises the trigger enter event to close the door once the player goes through
	/// </summary>
	/// <param name="other">Other.</param>
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
