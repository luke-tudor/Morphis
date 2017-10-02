using UnityEngine;
using System.Collections;

public class SpeedBlock : MonoBehaviour {

    public float SpeedMultiplier = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionStay(Collision collisionInfo)
    {
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        

    }
}
