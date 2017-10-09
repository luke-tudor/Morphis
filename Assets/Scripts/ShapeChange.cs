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

    /// <summary>
    /// Size to grow or shrink towards.
    /// </summary>
    private int _desiredScale;

    private Transform _transform;
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
    }

    /// <summary>
    /// Grow the block
    /// </summary>
    public void Grow()
    {
        if (!Extrudable || _collisionDetected)
            return;

        _grownThisUpdate = true;
        _desiredScale = Mathf.CeilToInt(transform.localScale.y + 0.001f);
    }

    /// <summary>
    /// Shrink the block
    /// </summary>
    public void Shrink()
    {
        if (!Extrudable)
            return;

        _collisionDetected = false;
        _desiredScale = Mathf.FloorToInt(transform.localScale.y - 0.001f);
    }
}