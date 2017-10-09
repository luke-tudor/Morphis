using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class TriggerGameOver : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ExitLevelTrigger"))
        {

            SceneManager.LoadScene("EndGame");

        }
    }
}

