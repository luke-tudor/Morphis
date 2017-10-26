using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// This class handles killing the player if they fall out of the level
/// </summary>
public class KillIfTooLow : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        Vector3 position = GetComponent<Transform>().position;	
        if (position.y < -200)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	}
}
