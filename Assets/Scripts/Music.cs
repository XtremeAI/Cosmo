using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {
	private static Music _instance;

	public static Music Instance {get { return _instance;}}

  private void Awake()
  {
    if (_instance != this && _instance != null)
    {
      Destroy(this.gameObject);
    }
    else
    {
      _instance = this;
      DontDestroyOnLoad(this.gameObject);
    }
  }
}
