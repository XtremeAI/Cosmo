using UnityEngine;

public class DamageDealer : MonoBehaviour {
  [SerializeField] int _damageLevel = 100;
  [SerializeField] bool _isPlayerOrigin;

  public int GetDamage() {
    return _damageLevel;
  }

  public void Hit() {
    Destroy(gameObject);
  }

  public bool IsPlayerOrigin() {
    return _isPlayerOrigin;
  }
}