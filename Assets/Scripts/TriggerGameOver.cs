using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class TriggerGameOver : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("WHAT");
        Debug.Log(other.tag);
        if (other.CompareTag("ExitLevelTrigger"))
        {

            SceneManager.LoadScene("EndGame");

        }
    }
}

