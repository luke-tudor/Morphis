using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Highlight the object this is attached to with a specific color
/// </summary>
public class Highlight : MonoBehaviour
{

    public Color HighlightColor = new Color(1f, 0f, 0f);

    /// <summary>
    ///  Should gameobject be highlighted
    /// </summary>
    private bool _highlighted;

    /// <summary>
    /// Remember colors of all the materials so we can change back
    /// </summary>
    private IDictionary<Renderer, Color> _defaultColors;
    private Renderer[] _renderers;

    // Use this for initialization
    void Start()
    {
        // Record colors of all objects
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
            // Change rendered materials to highlight color
            foreach (Renderer renderer in _renderers)
                renderer.material.color = HighlightColor;
        }
        else
        {
            // Change materials back to default color
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

    /// <summary>
    /// Set the current gameobject to be highlighted.
    /// </summary>
    /// <param name="value">Whether to highlight or not.</param>
    public void SetHighlight(bool value)
    {
        _highlighted = value;
    }
}