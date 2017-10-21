using UnityEngine;
using System.Collections;

/*
 * Class that is designed to rotate a camera by a specified amount each frame.
 * Could be used for other purposes however. 
 */
public class RotateCamera : MonoBehaviour {

    public float rotateSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, rotateSpeed, 0) * Time.deltaTime);
	}
}
