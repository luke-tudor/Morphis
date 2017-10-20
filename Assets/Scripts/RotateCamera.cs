using UnityEngine;
using System.Collections;

/*
 * Class that is designed to rotate a camera by a specified amount each frame.
 * Could be used for other purposes however. 
 */
public class RotateCamera : MonoBehaviour {

    public float rotateSpeed;

    private Vector3 transformVector;

	// Use this for initialization
	void Start () {
        transformVector = new Vector3(0, rotateSpeed, 0) * Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(transformVector);
	}
}
