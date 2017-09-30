using UnityEngine;
using System.Collections;

public class WandPointer : MonoBehaviour
{

    private Transform _transform;

    // Use this for initialization
    void Start()
    {
        _transform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(_transform.position, _transform.forward);
        RaycastHit hitInfo;

        Physics.Raycast(ray, out hitInfo);

        if (hitInfo.collider != null)
        {

            GameObject hitObject = hitInfo.collider.gameObject;

            ShapeChange shapeChange;
            Highlight highlight;

            if ((shapeChange = hitObject.GetComponentInParent<ShapeChange>()) != null)
            {
                // Left mouse down
                if (Input.GetMouseButton(0))
                {
                    shapeChange.Grow();
                }

                // Right mouse down
                if (Input.GetMouseButton(1))
                {
                    shapeChange.Shrink();
                }
            }

            if ((highlight = hitObject.GetComponentInParent<Highlight>()) != null)
            {
                highlight.SetHighlight(true);
            }
        }
    }
}