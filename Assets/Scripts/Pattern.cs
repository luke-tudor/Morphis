using UnityEngine;
using System.Collections;

/// <summary>
/// This class checks if pattern puzzles have been solved
/// </summary>
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

	/// <summary>
	/// Checks if the player has solved the puzzle
	/// </summary>
	/// <returns><c>true</c>, if if solved was checked, <c>false</c> otherwise.</returns>
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

	/// <summary>
	/// Opens the door once the puzzle is solved
	/// </summary>
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
