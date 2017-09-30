using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Highlight : MonoBehaviour
{

    public Color HighlightColor = new Color(1f, 0f, 0f);

    private bool _highlighted;
    private IDictionary<Renderer, Color> _defaultColors;
    private Renderer[] _renderers;

    // Use this for initialization
    void Start()
    {
        _defaultColors = new Dictionary<Renderer, Color>();

        _renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in _renderers)
            _defaultColors.Add(renderer, renderer.material.color);
    }

    // Update is called once per frame
    void Update()
    {

        if (_highlighted)
        {

            foreach (Renderer renderer in _renderers)
                renderer.material.color = HighlightColor;

        }
        else
        {

            foreach (Renderer renderer in _renderers)
            {
                Color defaultColor;

                if (_defaultColors.TryGetValue(renderer, out defaultColor))
                {
                    renderer.material.color = defaultColor;
                }
            }

        }

        _highlighted = false;
    }

    public void SetHighlight(bool value)
    {
        _highlighted = value;
    }
}