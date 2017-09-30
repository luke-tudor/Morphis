using UnityEngine;
using System.Collections;

public class ShapeChange : MonoBehaviour
{

    public float GrowthRate = 1.0f;

    public float MinSize = 0.000001f;
    public float MaxSize = 10f;

    private Transform _transform;

    private bool _grownThisUpdate = false;
    private bool _shrunkThisUpdate = false;

    // Use this for initialization
    void Start()
    {
        _transform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _shrunkThisUpdate = false;
        _grownThisUpdate = false;
    }

    public void Grow()
    {
        if (_grownThisUpdate)
            return;

        Vector3 newScale = _transform.localScale + (Vector3.up * (Time.deltaTime * GrowthRate));
        newScale.y = Mathf.Min(newScale.y, MaxSize);

        _transform.localScale = newScale;

        _grownThisUpdate = true;
    }

    public void Shrink()
    {
        if (_shrunkThisUpdate)
            return;

        Vector3 newScale = _transform.localScale - (Vector3.up * (Time.deltaTime * GrowthRate));
        newScale.y = Mathf.Max(newScale.y, MinSize);

        _transform.localScale = newScale;

        _shrunkThisUpdate = true;
    }
}