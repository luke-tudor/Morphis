using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// Detect player collisions with blocks with affects and apply those affects
/// </summary>
public class AffectorBlockCollisionDetection : MonoBehaviour
{

    CharacterController _characterController;
    FirstPersonController _firstPersonController;
    Rigidbody _rigidbody;

    public bool SpeedAffects;
    public bool JumpAffects;
    public bool FrictionAffects;

    // Use this for initialization
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _firstPersonController = GetComponent<FirstPersonController>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Frictionless blocks
        if (hit.gameObject.transform.parent.tag == Tags.FRICTIONLESS_BLOCK && FrictionAffects)
        {
            //_firstPersonController.setOnFrictionlessBlock();
        }

        // Speed blocks
        if (hit.gameObject.tag == Tags.SPEED_BLOCK && SpeedAffects)
        {
            // Change player speed
            _firstPersonController.BoostSpeed(2);
        }

        // Jump blocks
        if (hit.gameObject.tag == Tags.JUMP_BLOCK && JumpAffects)
        {
            // Add upwards velocity to player controller to make them jump
            _firstPersonController.AddVelocity(this.transform.up * 35);
        }
    }
}