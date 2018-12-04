using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  [SerializeField] float _moveSpeed = 10;
	[SerializeField] int _health = 700;
  [SerializeField] float _laserSpeed = 20;
  [SerializeField] float _laserFiringPeriod = 0.2f;
	[SerializeField] float _laserLifeTime = 2f;
	[SerializeField] GameObject _laserPrefab;
  [SerializeField] AudioClip _laserSound;
  [SerializeField] float _explosionLifeTime = 1f;
  [SerializeField] GameObject _explosionPrefab;
  [SerializeField] AudioClip _explosionSound;
	private float xMin;
  private float xMax;
  private float yMin;
  private float yMax;
  private float spriteSizeX;
  private float spriteSizeY;
  private float projectileSpeed;

  Coroutine firingCoroutine;

  // Use this for initialization
  void Start () {
		SetupMoveBoundaries();

		 
	}

  private void SetupMoveBoundaries()
  {
		var spriteComponent = GetComponent<SpriteRenderer>();
    spriteSizeX = spriteComponent.size.x;
    spriteSizeY = spriteComponent.size.y;

    Camera gameCamera = Camera.main;
    xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + spriteSizeX / 2;
    xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - spriteSizeX / 2;
    yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + spriteSizeY / 2;
    yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - spriteSizeY / 2;
  }

  // Update is called once per frame
  void Update () {
		Move();
		Fire();
	}

  private void Fire()
  {
    if (Input.GetButtonDown("Fire1")) {
      firingCoroutine = StartCoroutine(FireContinuously());
    }
		if (Input.GetButtonUp("Fire1")) {
			StopCoroutine(firingCoroutine);
		}
  }

  IEnumerator FireContinuously()
  {
		while (true) {
			GameObject laser = Instantiate(_laserPrefab, transform.position, Quaternion.identity).gameObject;
			var laserVelocity = new Vector2(0, _laserSpeed);
			laser.GetComponent<Rigidbody2D>().velocity = laserVelocity;
			Destroy(laser, _laserLifeTime);

      // Audio
      AudioSource.PlayClipAtPoint(_laserSound, laser.transform.position);
			
			yield return new WaitForSeconds(_laserFiringPeriod);
		}
  }

  private void Move()
  {

    var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * _moveSpeed;
    var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * _moveSpeed;

    var newXPos = Mathf.Clamp((transform.position.x + deltaX), xMin, xMax);
    var newYPos = Mathf.Clamp((transform.position.y + deltaY), yMin, yMax);
		var newPosition = new Vector2(newXPos, newYPos);
		transform.position = newPosition;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    DamageDealer damageDealer = other.GetComponent<DamageDealer>();

    if (damageDealer != null)
    {
      if (!damageDealer.IsPlayerOrigin())
      {
        ProcessHit(damageDealer);
      }
    }
  }

  private void ProcessHit(DamageDealer damageDealer)
  {
    _health -= damageDealer.GetDamage();
    damageDealer.Hit();
    if (_health <= 0)
    {
      GameObject explosionObj = Instantiate(
        _explosionPrefab,
        transform.position,
        Quaternion.identity
      );

      Destroy(explosionObj, _explosionLifeTime);

      // Audio
      AudioSource.PlayClipAtPoint(_explosionSound, transform.position);

      GetComponent<SpriteRenderer>().enabled = false;
      GetComponent<Collider2D>().enabled = false;
      StartCoroutine(DieDelay());
    }
  }

	IEnumerator DieDelay() {
		yield return new WaitForSeconds(2f);
		FindObjectOfType<SceneLoader>().LoadNextScene();
  }

}
