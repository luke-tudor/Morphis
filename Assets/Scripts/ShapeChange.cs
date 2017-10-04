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

    private bool _grownThisUpdate = false;
    private bool _shrunkThisUpdate = false;
	private bool _collisionDetected = false;

    // Use this for initialization
    void Start()
    {
        _transform = GetComponent<Transform>();
        _desiredScale = (int)transform.localScale.y;
    }

	void OnTriggerEnter(Collider collision)
	{
		if (_grownThisUpdate && collision.gameObject.tag != "Untagged") {
			_collisionDetected = true;
		}
	}

	void OnTriggerExit(Collider collision) {
		if (collision.gameObject.tag != "Untagged") {
			_collisionDetected = false;
		}
	}

    // Update is called once per frame
    void Update()
    {
        if (Extrudable && !Mathf.Approximately(_desiredScale, transform.localScale.y))
        {
            Vector3 newScale = _transform.localScale;

            newScale.y = Mathf.MoveTowards(newScale.y, _desiredScale, Time.deltaTime * GrowthRate);
            newScale.y = Mathf.Clamp(newScale.y, MinSize, MaxSize);

            _transform.localScale = newScale;
        }
    }

    public void Grow()
    {
        if (!Extrudable)
            return;

		if (_grownThisUpdate || _collisionDetected) {
			return;
		}

        _desiredScale = Mathf.CeilToInt(transform.localScale.y + 0.001f);
    }

    public void Shrink()
    {      
        if (!Extrudable)
            return;

        _desiredScale = Mathf.FloorToInt(transform.localScale.y - 0.001f);
    }
}