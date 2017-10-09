using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayStarsCollected : MonoBehaviour {

    private Text instruction;
    void Start()
    {
        instruction = GetComponent<Text>();
        instruction.text = "You collected " + new StarSpawner().GetStarCount() + " stars!";
    }

    // Update is called once per frame
    void Update () {
	
	}
}
