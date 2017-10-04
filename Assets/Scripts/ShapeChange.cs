﻿using UnityEngine;
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

    // Use this for initialization
    void Start()
    {
        _transform = GetComponent<Transform>();
        _desiredScale = (int)transform.localScale.y;
        _renderers = GetComponentsInChildren<Renderer>();

        _defaultMatColors = new Dictionary<Renderer, Color>();

        foreach (Renderer renderer in _renderers)
        {
            _defaultMatColors.Add(renderer, renderer.material.color);
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

        _desiredScale = Mathf.CeilToInt(transform.localScale.y + 0.001f);
    }

    public void Shrink()
    {      
        if (!Extrudable)
            return;

        _desiredScale = Mathf.FloorToInt(transform.localScale.y - 0.001f);
    }

    void OnValidate()
    {
        Debug.Log("Foo");

        if (!Extrudable)
        {
            foreach (Renderer renderer in _renderers)
            {
                Color defaultColor = _defaultMatColors[renderer];
                Color newColor = new Color(defaultColor.r - 0.8f, defaultColor.g - 0.8f, defaultColor.b - 0.8f);
                renderer.material.color = newColor;
            }
        } else
        {
            foreach (Renderer renderer in _renderers)
            {
                renderer.material.color = _defaultMatColors[renderer];
            }
        }
    }
}