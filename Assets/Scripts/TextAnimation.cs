using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour {

	[SerializeField] Text _text;
	[SerializeField] Color _textColor;
	[SerializeField] float _textOpacity = 0f;
	[SerializeField] float _changeSpeed = 0.1f;

	// Use this for initialization
	void Start () {
		_text = gameObject.GetComponent<Text>();
		_textColor = new Color(_text.color.r, _text.color.g, _text.color.b, _textOpacity);
		_text.color = new Color(_textColor.r * _textOpacity, _textColor.g * _textOpacity, _textColor.b * _textOpacity, _textOpacity);
	}
	
	// Update is called once per frame
	void Update () {
		_textOpacity = Mathf.Min(_textOpacity + Time.deltaTime, 1.0f);
		
		_text.color = new Color(_textColor.r * _textOpacity, _textColor.g * _textOpacity, _textColor.b * _textOpacity, _textOpacity);
	}
}
