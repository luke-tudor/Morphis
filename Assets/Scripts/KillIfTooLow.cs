using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
