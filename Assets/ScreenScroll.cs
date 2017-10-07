using UnityEngine;
using System.Collections;

public class ScreenScroll : MonoBehaviour {

    public float ScrollSpeed = 0.1f;

    private Renderer _renderer;

	// Use this for initialization
	void Start () {
        _renderer = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 offset = _renderer.material.mainTextureOffset;
        offset += new Vector2(0, ScrollSpeed) * Time.deltaTime;
        Debug.Log("Offset = " + offset);
        _renderer.material.mainTextureOffset = offset;
    }
}
