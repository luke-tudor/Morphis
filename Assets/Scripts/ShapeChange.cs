using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Grow and/or shrink blocks
/// </summary>
[ExecuteInEditMode]
public class ShapeChange : MonoBehaviour
{
	/// <summary>
	/// Is the block growable/shrinkable
	/// </summary>
	public bool Extrudable = true;

	/// <summary>
	/// Rate at which to grow or shrink
	/// </summary>
	public float GrowthRate = 1.0f;

	/// <summary>
	/// Minimum shrinkage size
	/// </summary>
	public float MinSize = 0.000001f;

	/// <summary>
	/// Maximum growth size
	/// </summary>
	public float MaxSize = 10f;

	public AudioSource _grindingNoise;

	/// <summary>
	/// Size to grow or shrink towards.
	/// </summary>
	private int _desiredScale;

	public List<ShapeChange> _linkedShapes;
	public bool _isLinked = false;
	private Transform _transform;
	private IDictionary<Renderer, Color> _defaultMatColors;
	private Renderer[] _renderers;
	private bool _collisionDetected;
	private bool _grownThisUpdate;
	private bool _growsDown;
	private bool _growsUp;

	// Use this for initialization
	void Start()
	{
		_transform = GetComponent<Transform>();
		_desiredScale = (int)transform.localScale.y;
		_collisionDetected = false;
		_grownThisUpdate = false;

		Vector3 eulerAngles = transform.rotation.eulerAngles;
		eulerAngles.x = Mathf.CeilToInt (eulerAngles.x);
		eulerAngles.y = Mathf.CeilToInt (eulerAngles.y);
		eulerAngles.z = Mathf.CeilToInt (eulerAngles.z);
		_growsDown = eulerAngles.y == 180 && eulerAngles.z == 180;
		_growsUp = eulerAngles.x == 0 && eulerAngles.z == 0;
	}

	void OnTriggerEnter(Collider collision)
	{
		// Move the player back when it collides with the growing block

		if (collision.name == "Player")
		{
			if (_growsUp) {
				return;
			}

			if (!PlayerWillBePushedOutOfBounds ()) {
				Vector3 closestPoint = collision.ClosestPointOnBounds(this.gameObject.transform.position);
				closestPoint.y = collision.gameObject.transform.position.y;

				Vector3 eulerAngles = transform.rotation.eulerAngles;
				Vector3 newPosition = collision.gameObject.transform.position;

				if (Mathf.Approximately(eulerAngles.y, 0)) {
					newPosition.z -= 1.5f;
				} else if (Mathf.Approximately(eulerAngles.y, 90)) {
					newPosition.x -= 1.5f;
				} else if (Mathf.Approximately(eulerAngles.y, 180)) {
					newPosition.z += 1.5f;
				} else if (Mathf.Approximately(eulerAngles.y, 270)) {
					newPosition.x += 1.5f;
				}

				collision.gameObject.transform.position = newPosition;
				return;

			}

			collision.BroadcastMessage ("BoostSpeed", 0, SendMessageOptions.DontRequireReceiver);
			Vector3 newScale = _transform.localScale;
			newScale.y = _desiredScale - 0.5f;
			newScale.y = Mathf.Clamp(newScale.y, MinSize, MaxSize);
			_transform.localScale = newScale;
		}

		if (_grownThisUpdate)
		{
			_collisionDetected = true;
		}
	}

	void OnTriggerExit(Collider collision)
	{
		if (collision.name == "Cube" || collision.name == "Player" && !PlayerWillBePushedOutOfBounds())
		{
			_collisionDetected = false;
		}
	}

	// Update is called once per frame
	void Update()
	{

		// If current size is not the desired size then scale towards the desired size
		if (!_collisionDetected && (Extrudable || !Extrudable && _isLinked) && !Mathf.Approximately(_desiredScale, transform.localScale.y))
		{
			Vector3 newScale = _transform.localScale;

			newScale.y = Mathf.MoveTowards(newScale.y, _desiredScale, Time.deltaTime * GrowthRate);

			// Clamp max and min size
			newScale.y = Mathf.Clamp(newScale.y, MinSize, MaxSize);

			_transform.localScale = newScale;
		}
		else if(_grindingNoise != null)
		{
			if (_grindingNoise.isPlaying)
			{
				_grindingNoise.Stop();
			}
		}

		if ((Mathf.Approximately(transform.localScale.y, MinSize) || Mathf.Approximately(transform.localScale.y, MaxSize) ) && _grindingNoise != null && _grindingNoise.isPlaying)
		{
			_grindingNoise.Stop();
		}
	}

	/// <summary>
	/// Grow the block
	/// </summary>
	public void Grow()
	{
		
		if (!Extrudable && !_isLinked || _collisionDetected)
			return;

		if (_linkedShapes != null && _linkedShapes.Count > 0) {
			for (int i = 0; i < _linkedShapes.Count; i++) {
				if (_linkedShapes [i] != null) {
					_linkedShapes [i].Grow ();
				}
			}
		}
			
		if (_growsDown) {
			GameObject person = GameObject.Find("Player");
			double heightDifference = this.gameObject.transform.position.y - GetComponent<Collider> ().bounds.size.y - person.transform.position.y;
			if (heightDifference <= 3.2 && isInBoundsOfObject(person)) {
				return;
			}
		}

		if (_growsUp) {
			RaycastHit hit;
			if(Physics.Raycast (GetComponent<Collider> ().bounds.center, Vector3.up, out hit)) {
				GameObject person = GameObject.Find("Player");
				double heightDifference = hit.collider.gameObject.transform.position.y - person.transform.position.y;
				if (heightDifference <= 3 && isInBoundsOfObject(person)) {
					return;
				}
			}
		}

		_grownThisUpdate = true;
		_desiredScale = Mathf.CeilToInt(transform.localScale.y + 0.001f);

		if (!_grindingNoise.isPlaying)
		{
			_grindingNoise.Play();
		}
	}

	/// <summary>
	/// Shrink the block
	/// </summary>
	public void Shrink()
	{
		if (_linkedShapes != null && _linkedShapes.Count > 0) {
			for (int i = 0; i < _linkedShapes.Count; i++) {
				if (_linkedShapes [i] != null) {
					_linkedShapes [i].Shrink ();
				}
			}
		}

		if (!Extrudable && !_isLinked)
			return;

		_collisionDetected = false;
		_desiredScale = Mathf.FloorToInt(transform.localScale.y - 0.001f);

		if (!_grindingNoise.isPlaying)
		{
			_grindingNoise.Play();
		}
	}

	public void SetDesiredSize(int desiredScale)
	{
		_desiredScale = desiredScale;
	}

	private bool isInBoundsOfObject (GameObject obj) {
		double midZ = GetComponent<Collider> ().bounds.center.z;
		double lowZ = midZ - GetComponent<Collider> ().bounds.size.z / 2;
		double highZ = midZ + GetComponent<Collider> ().bounds.size.z / 2;

		double midX = GetComponent<Collider> ().bounds.center.x;
		double lowX = midX - GetComponent<Collider> ().bounds.size.x / 2;
		double highX = midX + GetComponent<Collider> ().bounds.size.x / 2;

		bool playerIsInXBounds = lowX <= obj.transform.position.x && obj.transform.position.x <= highX;
		bool playerIsInZBounds = lowZ <= obj.transform.position.z && obj.transform.position.z <= highZ;

		return playerIsInXBounds && playerIsInZBounds;
	}

	private bool PlayerWillBePushedOutOfBounds () {
		RaycastHit hit;
		Vector3 direction = GetComponent<Collider> ().bounds.center;
		direction.Scale (transform.rotation.eulerAngles.normalized);

		Vector3 eulerAngles = transform.rotation.eulerAngles;
		Vector3 rayDirection = Vector3.one;
		Vector3 origin = GetComponent<Collider> ().bounds.center;

		if (Mathf.Approximately(eulerAngles.y, 0)) {
			rayDirection = Vector3.back;
			float z = (transform.position.z - GetComponent<Collider> ().bounds.center.z) * 2;
			origin.z = transform.position.z - z;
		} else if (Mathf.Approximately(eulerAngles.y, 90)) {
			rayDirection = Vector3.left;
			float x = (transform.position.x - GetComponent<Collider> ().bounds.center.x) * 2;
			origin.x = transform.position.x - x;
		} else if (Mathf.Approximately(eulerAngles.y, 180)) {
			rayDirection = Vector3.forward;
			float z = (transform.position.z - GetComponent<Collider> ().bounds.center.z) * 2;
			origin.z = transform.position.z - z;
		} else if (Mathf.Approximately(eulerAngles.y, 270)) {
			rayDirection = Vector3.right;
			float x = (transform.position.x - GetComponent<Collider> ().bounds.center.x) * 2;
			origin.x = transform.position.x - x;
		}

		if(Physics.Raycast (origin, rayDirection, out hit)) {
			if (hit.distance <= 3) {
				return true;
			}
		}
		return false;
	}

    public void setExtrudable(bool val)
    {
        Extrudable = val;
    }
}