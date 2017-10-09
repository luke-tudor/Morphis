using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayStarsCollected : MonoBehaviour {

    private Text instruction;
    private int starsCollected = 0;
 
    void Start()
    {
        instruction = GetComponent<Text>();
        instruction.text = "You collected " + PlayerPrefs.GetInt("stars") + " stars!";
        GameObject.Find("StarSpawner");

    }

    // Update is called once per frame
    void Update () {
	
	}

    public void updateStars()
    {
        starsCollected++;
    }
}
