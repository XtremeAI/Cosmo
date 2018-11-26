using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

	WaveConfig _waveConfig;
	List<Transform> _wayPoints;

	private void Start() {
		_wayPoints = _waveConfig.GetWaypoints();
    transform.position = _wayPoints[0].position;
		_wayPoints.RemoveAt(0);
	}

	public void SetWaveConfig(WaveConfig wafeConfig) {
		_waveConfig = wafeConfig;
	}

	private void Update() {

		float step = _waveConfig.GetMoveSpeed() * Time.deltaTime;

		transform.position = Vector2.MoveTowards(transform.position, _wayPoints[0].position, step);

		if (transform.position == _wayPoints[0].position) {
			_wayPoints.RemoveAt(0);
		}

		if (_wayPoints.Count == 0) {
			Destroy(gameObject);
		}
	}
}
