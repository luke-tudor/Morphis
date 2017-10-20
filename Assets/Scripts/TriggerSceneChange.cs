using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


/*
 * Class has methods to trigger another scene
 */
public class TriggerSceneChange : MonoBehaviour
{
    public string NextLevel;
    // If the collision is with the player load the next scene
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("hello");
        if (other.CompareTag("Player"))
        {
            Debug.Log("hello1");
            SceneManager.LoadScene(NextLevel);
        }
    }
}

