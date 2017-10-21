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

	private int counter;
	private bool increasing;

	private float RValue;
	private float GValue;
	private float BValue;

    // Use this for initialization
    void Start()
    {
        // Record colors of all objects
        _defaultColors = new Dictionary<Renderer, Color>();
        _renderers = GetComponentsInChildren<Renderer>();

		counter = 1;
		increasing = true;

		getColourValues();

        foreach (Renderer renderer in _renderers)
            _defaultColors.Add(renderer, renderer.material.color);
    }

    // Update is called once per frame
    void Update()
    {

		//Debug.Log (RValue);

        if (_highlighted)
        {
            // Change rendered materials to highlight color

			if (increasing) {
				if (counter <= 100) {
					counter += 5;
				}
			} 
			else {
				counter -= 5;
			}

			if (counter >= 100) {
				increasing = false;
			} else if (counter <= 1) {
				increasing = true;
			}

			if (counter < 0) {
				counter = 1;
			}

			float r = 1 - RValue;
			float g = 1 - GValue;
			float b = 1 - BValue;

			float y = 100;

			float x = counter / y;


			foreach (Renderer renderer in _renderers)
				renderer.material.color = new Color (RValue+(x*r), GValue+(x*g), BValue+(x*b));


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

	private void getColourValues()
	{

		foreach (Renderer renderer in _renderers) {
			Color defaultColour;
			_defaultColors.TryGetValue (renderer, out defaultColour);

			RValue = defaultColour.r;
			GValue = defaultColour.g;
			BValue = defaultColour.b;
		}


	}
}