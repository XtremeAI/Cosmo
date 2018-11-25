using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

	[SerializeField] List<Transform> _wayPoints;
	[SerializeField] float _speed = 2f;

	private void Start() {
    transform.position = _wayPoints[0].position;
		_wayPoints.RemoveAt(0);
	}

	private void Update() {

		float step = _speed * Time.deltaTime;

		transform.position = Vector2.MoveTowards(transform.position, _wayPoints[0].position, step);

		if (transform.position == _wayPoints[0].position) {
			_wayPoints.RemoveAt(0);
		}

		if (_wayPoints.Count == 0) {
			Destroy(gameObject);
		}
	}
}
