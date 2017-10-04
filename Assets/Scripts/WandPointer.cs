using UnityEngine;
using System.Collections;

public class WandPointer : MonoBehaviour
{

    private Transform _transform;
	private ShapeChange _shapeChange;
	private Highlight _highlight;

    // Use this for initialization
    void Start()
    {
        _transform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
		if (_shapeChange != null) {
			if (Input.GetMouseButton(0)) {
				_shapeChange.Grow();
				HighLight ();
				return;
			} else if (Input.GetMouseButton(1)) {
				_shapeChange.Shrink();
				HighLight ();
				return;
			} else {
				_highlight = null;
				_shapeChange = null;
			}
		}

        Ray ray = new Ray(_transform.position, _transform.forward);
        RaycastHit hitInfo;

        Physics.Raycast(ray, out hitInfo);

        if (hitInfo.collider != null)
        {

            GameObject hitObject = hitInfo.collider.gameObject;

            Highlight highlight;
			_shapeChange = hitObject.GetComponentInParent<ShapeChange> ();

			if (_shapeChange != null)
            {
                // Left mouse down
                if (Input.GetMouseButton(0))
                {
					_shapeChange.Grow();
                }

                // Right mouse down
                if (Input.GetMouseButton(1))
                {
					_shapeChange.Shrink();
                }
            }
			_highlight = hitObject.GetComponentInParent<Highlight> ();
			HighLight();
        }
    }

	void HighLight () {
		if (_highlight != null)
		{
			_highlight.SetHighlight(true);
		}
	}
}