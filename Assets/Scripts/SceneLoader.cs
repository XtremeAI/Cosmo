using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public void LoadMenu() {
		SceneManager.LoadScene(0);
	}

	public void LoadGame() {
		SessionState.Instance.ResetSession();
		SceneManager.LoadScene(1);
	}

	public void LoadDelayedGameOver() {
		StartCoroutine(DelayedGameOverCoroutine());
	}

	IEnumerator DelayedGameOverCoroutine() {
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(2);
	}

	public void Exit() {
		Application.Quit();
	}
}
