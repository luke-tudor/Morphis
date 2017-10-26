using UnityEngine;
using System.Collections;

/// <summary>
/// Handles spawning of a star in a certain position
/// </summary>
public class StarSpawnPosition : MonoBehaviour {
	public GameObject star;

	//Method called when this star spawn position is chosen to spawn a star
	void Spawn(){
		//Spawns a star object in the same position as this spawn position
		star = GameObject.FindGameObjectWithTag ("Star");
		Instantiate (star, transform.position, transform.rotation);
	}
}
