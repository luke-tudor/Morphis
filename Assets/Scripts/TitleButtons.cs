using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


/*
 * Class that adds methods that are called when the respective buttons are clicked
 */
public class TitleButtons : MonoBehaviour
{
    public string NextLevel;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Load the prototype level
    public void Play()
    {
        if (NextLevel != null && NextLevel != "")
        {
            SceneManager.LoadScene(NextLevel);
        }
        else {
            SceneManager.LoadScene(PlayerPrefs.GetString("NextLevel"));
        }
    }

    // Quit the game
    public void Quit()
    {
        Application.Quit();
    }

}