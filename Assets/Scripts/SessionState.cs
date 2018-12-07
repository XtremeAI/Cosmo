using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionState : MonoBehaviour {

	static private SessionState _instance;

	static public SessionState Instance { get { return _instance; } }

	public int Score { get; private set; }

	private void Awake() {
		if (_instance != this && _instance != null)
		{
			Destroy(this.gameObject);
		}
		else {
			_instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
	}	
	
	public void AddScore(int score) {
		Score += score;
	}

	public void ResetSession() {
		Score = 0;
	}
}
