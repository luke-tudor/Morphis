using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ShapeChange : MonoBehaviour
{

    public bool Extrudable = true;

    public float GrowthRate = 1.0f;

    public float MinSize = 0.000001f;
    public float MaxSize = 10f;

    private Transform _transform;
    private float _desiredScale;
    private IDictionary<Renderer, Color> _defaultMatColors;
    private Renderer[] _renderers;
	private bool _collisionDetected;
	private bool _grownThisUpdate;

    public AudioSource grindingNoise;
    
    // Use this for initialization
    void Start()
    {
        _transform = GetComponent<Transform>();
        _desiredScale = (int)transform.localScale.y;
		_collisionDetected = false;
		_grownThisUpdate = false;
    }

	void OnTriggerEnter(Collider collision)
	{
		if (collision.name == "Player") {
			Vector3 closestPoint = collision.ClosestPointOnBounds(this.gameObject.transform.position);
			closestPoint.y = collision.gameObject.transform.position.y;
			Vector3 newPosition = collision.gameObject.transform.position - closestPoint;
			newPosition = newPosition * 2;
			newPosition += collision.gameObject.transform.position;
			collision.gameObject.transform.position = newPosition;
			return;
		}

		if (_grownThisUpdate) {
			_collisionDetected = true;
		}
	}

	void OnTriggerExit(Collider collision) {
		if (collision.name == "Cube") {
			_collisionDetected = false;
		}
	}

    // Update is called once per frame
    void Update()
    {
		if (!_collisionDetected && Extrudable && !Mathf.Approximately(_desiredScale, transform.localScale.y))
        {
            Vector3 newScale = _transform.localScale;

            newScale.y = Mathf.MoveTowards(newScale.y, _desiredScale, Time.deltaTime * GrowthRate);
            newScale.y = Mathf.Clamp(newScale.y, MinSize, MaxSize);

            _transform.localScale = newScale;

        } else if (grindingNoise != null)
        {
            if (grindingNoise.isPlaying)
            {
                grindingNoise.Stop();
            }
        }
    }

    public void Grow()
    {
		if (!Extrudable || _collisionDetected)
            return;
		
		_grownThisUpdate = true;
		_desiredScale = Mathf.CeilToInt(transform.localScale.y + 0.001f);

        if (!grindingNoise.isPlaying)
            grindingNoise.Play();
    }

    public void Shrink()
    {      
        if (!Extrudable)
            return;

		_collisionDetected = false;
		_desiredScale = Mathf.FloorToInt(transform.localScale.y - 0.001f);
        _desiredScale = Mathf.Max(0.001f, _desiredScale);

        if(!grindingNoise.isPlaying)
            grindingNoise.Play();
    }
}