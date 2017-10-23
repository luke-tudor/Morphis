using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


/*
 * Class has methods to trigger another scene
 */
public class TriggerSceneChange : MonoBehaviour
{
    public string NextScene;
    public string NextLevel;

    // Update what level is next
    void Start()
    {
        PlayerPrefs.SetString("NextLevel", NextLevel);
        StarCount.starCount = 0;
    }
        // If the collision is with the player load the next scene
        void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(NextScene);
        }
    }
}

