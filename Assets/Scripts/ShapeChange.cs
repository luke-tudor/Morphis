using UnityEngine;
using System.Collections;

public class ShapeChange : MonoBehaviour
{

    public bool Extrudable = true;

    public float GrowthRate = 1.0f;

    public float MinSize = 0.000001f;
    public float MaxSize = 10f;

    private Transform _transform;
    private int _desiredScale;

    // Use this for initialization
    void Start()
    {
        _transform = GetComponent<Transform>();
        _desiredScale = (int)transform.localScale.y;
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

        _desiredScale = Mathf.CeilToInt(transform.localScale.y + 0.001f);
    }

    public void Shrink()
    {      
        if (!Extrudable)
            return;

        _desiredScale = Mathf.FloorToInt(transform.localScale.y - 0.001f);
    }
}