using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


/*
 * Class that adds methods that are called when the respective buttons are clicked
 */
public class TitleButtons : MonoBehaviour
{

    // Load the prototype level
    public void Play()
    {
        SceneManager.LoadScene("PrototypeLevel2");
    }

    // Quit the game
    public void Quit()
    {
        Application.Quit();
    }

}