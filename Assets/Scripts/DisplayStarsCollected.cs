using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Class that displays how many stars the player collected at the end of the level
/// </summary>
public class DisplayStarsCollected : MonoBehaviour {

    private Text text;

    // Get the amount of stars and update text
    void Start()
    {
        text = GetComponent<Text>();
        text.text = "You collected " + StarCount.starCount + " stars!";
    }
}
