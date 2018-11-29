using System;
using UnityEngine;

public class Enemy : MonoBehaviour {
  [SerializeField] int _health = 100;
  [SerializeField] float _minTimeBetweenShots = 0.2f;
  [SerializeField] float _maxTimeBetweenShots = 2f;
  [SerializeField] float _rndTimeBetweenShots;
  [SerializeField] float _laserVelocity = -6f;
  [SerializeField] float _laserLifeTime = 5f;
  [SerializeField] float _explosionLifeTime = 0.3f;
  [SerializeField] GameObject _laserPrefab;
  [SerializeField] AudioClip _laserSound;
  [SerializeField] GameObject _explosionPrefab;
  [SerializeField] AudioClip _explosionSound;

  private void Start() {
    Reload();
  }

  private void Update() {
    CountDownAndShoot();
  }

  private void CountDownAndShoot()
  {
    _rndTimeBetweenShots -= Time.deltaTime;

    if (_rndTimeBetweenShots <= 0f) {
      Fire();
      Reload();
    }
  }

  private void Fire()
  {
    GameObject laserObj = Instantiate(
      _laserPrefab,
      transform.position,
      Quaternion.identity
    );

    Vector2 laserVelocity = new Vector2(0f, _laserVelocity);
    laserObj.GetComponent<Rigidbody2D>().velocity = laserVelocity;
    Destroy(laserObj, _laserLifeTime);

    // Audio
    AudioSource.PlayClipAtPoint(_laserSound, transform.position);
  }

  private void Reload()
  {
    _rndTimeBetweenShots = UnityEngine.Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
  }

  private void OnTriggerEnter2D(Collider2D other) {
    DamageDealer damageDealer = other.GetComponent<DamageDealer>();

    if (damageDealer != null)
    {
      if (damageDealer.IsPlayerOrigin() == true) {
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
      Explode();
    }
  }

  private void Explode()
  {
    GameObject explosionObj = Instantiate(
      _explosionPrefab,
      transform.position,
      Quaternion.identity
    );

    Destroy(explosionObj, _explosionLifeTime);

    // Audio
    AudioSource.PlayClipAtPoint(_explosionSound, transform.position);

    // Remove itself
    Destroy(gameObject);
  }
}