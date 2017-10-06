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
    private int _desiredScale;
    private IDictionary<Renderer, Color> _defaultMatColors;
    private Renderer[] _renderers;
	private bool _collisionDetected;
	private bool _grownThisUpdate;

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
		if (_grownThisUpdate && collision.name != "Player") {
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
        }
    }

    public void Grow()
    {
		if (!Extrudable || _collisionDetected)
            return;
		
		_grownThisUpdate = true;
		_desiredScale = Mathf.CeilToInt(transform.localScale.y + 0.001f);
	}

    public void Shrink()
    {      
        if (!Extrudable)
            return;

		_collisionDetected = false;
		_desiredScale = Mathf.FloorToInt(transform.localScale.y - 0.001f);
    }
}