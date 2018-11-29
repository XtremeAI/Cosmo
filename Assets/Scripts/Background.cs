using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
	[SerializeField] float _scrollSpeed = 0.05f;
  Material _material;
  float _materialVerticalOffset = 0f;

	// Use this for initialization
	void Start () {
		_material = gameObject.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
    _materialVerticalOffset += _scrollSpeed * Time.deltaTime;
		Vector2 offsetVector = new Vector2(0, _materialVerticalOffset);
		_material.mainTextureOffset = offsetVector;
	}
}
