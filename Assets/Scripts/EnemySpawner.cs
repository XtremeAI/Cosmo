using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	[SerializeField] List<WaveConfig> _waveConfigs;
	[SerializeField] int _startingWave = 0;
	[SerializeField] bool _loopWaves = true;


	// Use this for initialization
	IEnumerator Start () {
		do {
			yield return StartCoroutine(SpawnAllWaves());
		}
		while (_loopWaves);
	}

  private IEnumerator SpawnAllEnemiesInWave(WaveConfig currentWare)
  {
		for (int i = 0; i < currentWare.GetNumberOfEnemies(); i++) {
			
			var newEnemy = Instantiate(currentWare.GetEnemyPrefab(), currentWare.GetWaypoints()[0].transform.position, Quaternion.identity);
			newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(currentWare);

			yield return new WaitForSeconds(currentWare.GetTimeBetweenSpawns());
		}
  }

	private IEnumerator SpawnAllWaves() {

		for(int i = _startingWave; i < _waveConfigs.Count; i++) {
			yield return StartCoroutine(SpawnAllEnemiesInWave(_waveConfigs[i]));
		}
	}

  // Update is called once per frame
  void Update () {
		
	}
}
