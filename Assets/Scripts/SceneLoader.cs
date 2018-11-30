using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public void LoadNextScene() {
		int countGameScenes = SceneManager.sceneCountInBuildSettings;
		int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene((currentSceneIdx + 1) % countGameScenes);
	}

	public void Exit() {
		Application.Quit();
	}
}
