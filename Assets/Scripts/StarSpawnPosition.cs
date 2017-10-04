using UnityEngine;
using System.Collections;

public class StarSpawnPosition : MonoBehaviour {
	public GameObject star;

	void Spawn(){
		star = GameObject.FindGameObjectWithTag ("Star");
		Instantiate (star, transform.position, transform.rotation);
	}
}
