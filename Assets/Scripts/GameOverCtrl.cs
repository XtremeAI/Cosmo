using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCtrl : MonoBehaviour {


	[SerializeField] Text _textGameOver;
	[SerializeField] Text _textScore;
	[SerializeField] Color _textColor;
	[SerializeField] float _textOpacity = 0f;
	[SerializeField] float _changeSpeed = 0.1f;

	// Use this for initialization
	void Start () {
		_textScore.text = SessionState.Instance.Score.ToString();

		_textColor = new Color(_textGameOver.color.r, _textGameOver.color.g, _textGameOver.color.b, _textOpacity);
		_textGameOver.color = new Color(_textColor.r * _textOpacity, _textColor.g * _textOpacity, _textColor.b * _textOpacity, _textOpacity);
	}
	
	// Update is called once per frame
	void Update () {
		_textOpacity = Mathf.Min(_textOpacity + Time.deltaTime, 1.0f);
		_textGameOver.color = new Color(_textColor.r * _textOpacity, _textColor.g * _textOpacity, _textColor.b * _textOpacity, _textOpacity);
	}

}
