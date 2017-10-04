using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class AffectorBlockCollisionDetection : MonoBehaviour {

    CharacterController _characterController;
    FirstPersonController _firstPersonController;
    Rigidbody _rigidbody;

    public bool SpeedAffects;
    public bool JumpAffects;

	// Use this for initialization
	void Start () {
        _characterController = GetComponent<CharacterController>();
        _firstPersonController = GetComponent<FirstPersonController>();
        _rigidbody = GetComponent<Rigidbody>();
	}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.transform.parent.tag == Tags.FRICTIONLESS_BLOCK) {
            _firstPersonController.setOnFrictionlessBlock();
		} 

        if (hit.gameObject.tag == Tags.SPEED_BLOCK && SpeedAffects)
        {
            _firstPersonController.BoostSpeed(2);
        }

        if (hit.gameObject.tag == Tags.JUMP_BLOCK && JumpAffects)
        {
            _firstPersonController.AddVelocity(this.transform.up * 20);
        }
    }

    float mass = 3.0F; // defines the character mass
    Vector3 impact = Vector3.zero;

    public void ApplyForce(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        impact += dir.normalized * force / mass;
    }

    void Update()
    {
        //Debug.Log("Impact = " + impact);
        // apply the impact force:
        //if (impact.magnitude > 0.2F) _characterController.Move(impact * Time.deltaTime);
        // consumes the impact energy each cycle:
        //impact = Vector3.Lerp(impact, Vector3.zero, Time.deltaTime);
    }
}
