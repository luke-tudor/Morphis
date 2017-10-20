using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Highlight the object this is attached to with a specific color
/// </summary>
public class Highlight : MonoBehaviour
{
	// highlight colour is 31, 15, 15 or 0.1216, 0.0588, 0.0588
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

	private Color baseColor;

    // Use this for initialization
    void Start()
    {
        // Record colors of all objects
        _defaultColors = new Dictionary<Renderer, Color>();
        _renderers = GetComponentsInChildren<Renderer>();

		counter = 1;
		increasing = true;

		foreach (Renderer renderer in _renderers) {
			_defaultColors.Add (renderer, renderer.material.color);
		}

		getColourValues();

		RValue = baseColor.r;
		GValue = baseColor.g;
		BValue = baseColor.b;
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
					counter += 4;
				}
			} 
			else {
				counter -= 4;
			}

			if (counter >= 100) {
				increasing = false;
			} else if (counter <= 1) {
				increasing = true;
			}

			if (counter < 0) {
				counter = 1;
			}

			float highlightR = 1f;
			float highlightG = 0.66f;
			float highlightB = 0.53f;

			float r = highlightR - RValue;
			float g = highlightG - GValue;
			float b = highlightB - BValue;

			float y = 100;

			float x = counter / y;

			Color newColor = new Color (RValue + (x * r), GValue + (x * g), BValue + (x * b));

			foreach (Renderer renderer in _renderers)
				renderer.material.color = newColor;


        }
        else
        {
            // Change materials back to default color
            foreach (Renderer renderer in _renderers)
            {
                Color defaultColor;

                if (_defaultColors.TryGetValue(renderer, out defaultColor))
                {

					baseColor = defaultColor;
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

		var random = _defaultColors.First ();

		baseColor = random.Value;

	}
}