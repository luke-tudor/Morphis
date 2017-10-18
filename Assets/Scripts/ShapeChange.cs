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
		_growsDown = transform.rotation.eulerAngles.y == 180 && transform.rotation.eulerAngles.z == 180;
		_growsUp = transform.rotation.eulerAngles.x == 0 && transform.rotation.eulerAngles.z == 0 && transform.rotation.eulerAngles.y == 0;
    }

    void OnTriggerEnter(Collider collision)
    {
        // Move the player back when it collides with the growing block
        
        if (collision.name == "Player")
        {
            Vector3 closestPoint = collision.ClosestPointOnBounds(this.gameObject.transform.position);
            closestPoint.y = collision.gameObject.transform.position.y;
            Vector3 newPosition = collision.gameObject.transform.position - closestPoint;
            newPosition = newPosition * 2;
            newPosition += collision.gameObject.transform.position;
            collision.gameObject.transform.position = newPosition;
            return;
        }

        if (_grownThisUpdate)
        {
            _collisionDetected = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.name == "Cube")
        {
            _collisionDetected = false;
        }
    }

    // Update is called once per frame
    void Update()
    {


        // If current size is not the desired size then scale towards the desired size
        if (!_collisionDetected && Extrudable && !Mathf.Approximately(_desiredScale, transform.localScale.y))
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

        if (Mathf.Approximately(transform.localScale.y, MinSize) && _grindingNoise != null && _grindingNoise.isPlaying)
        {
            _grindingNoise.Stop();
        }
    }

    /// <summary>
    /// Grow the block
    /// </summary>
	public void Grow()
	{
		if (_linkedShapes != null && _linkedShapes.Count > 0) {
			for (int i = 0; i < _linkedShapes.Count; i++) {
				if (_linkedShapes [i] != null) {
					_linkedShapes [i].Grow ();
				}
			}
		}

		if (!Extrudable || _collisionDetected)
			return;

		if (_growsDown) {
			GameObject person = GameObject.Find("Player");
			double heightDifference = this.gameObject.transform.position.y - GetComponent<Collider> ().bounds.size.y - person.transform.position.y;
			if (heightDifference <= 3.2 && isInBoundsOfObject(person)) {
				return;
			}
		}

		if (_growsUp) {
			RaycastHit hit;
			Vector3 origin = transform.position;
			origin.y += GetComponent<Collider> ().bounds.size.y;
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

        if (!Extrudable)
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
}