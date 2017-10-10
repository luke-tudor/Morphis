using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


/*
 * Class has methods to trigger end game scene
 */
public class TriggerGameOver : MonoBehaviour
{
    // If the collision is with the right object load endgame scene
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ExitLevelTrigger"))
        {

            SceneManager.LoadScene("EndGame");

        }
    }
}

