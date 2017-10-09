using UnityEngine;
using System.Collections;

/// <summary>
/// Player cursor pointer
/// </summary>
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
        // If we are already engaged in growing or shrinking a block then continue to do so even if the user looks away
        if (_shapeChange != null)
        {
            if (Input.GetMouseButton(0))
            { // Left mouse button down

                // Grow and highlight block
                _shapeChange.Grow();
                HighLight();
                return;
            }
            else if (Input.GetMouseButton(1))
            { // RIght mouse button down

                // Shrink and highlight block
                _shapeChange.Shrink();
                HighLight();
                return;
            }
            else {
                // If no mouse button down then stop highlight, growing or shrinking blocks
                _highlight = null;
                _shapeChange = null;
            }
        }

        // Get gameobject being looked at
        Ray ray = new Ray(_transform.position, _transform.forward);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);

        if (hitInfo.collider != null)
        {

            GameObject hitObject = hitInfo.collider.gameObject;

            Highlight highlight;
            _shapeChange = hitObject.GetComponentInParent<ShapeChange>();

            // Start growing and shrinking a block
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
            _highlight = hitObject.GetComponentInParent<Highlight>();
            HighLight();
        }
    }

    /// <summary>
    /// Set highlight
    /// </summary>
	void HighLight()
    {
        if (_highlight != null)
        {
            _highlight.SetHighlight(true);
        }
    }
}