using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float travelSpeed = 1f;
    [SerializeField] private float damage = 1f;

    private Transform spawnLocation;
    
    private Transform _target = null;

    void FixedUpdate()
    {
        if (_target != null) {
            Vector3 travelDirection = (_target.position - transform.position).normalized;

            transform.position = Vector3.Lerp(transform.position, _target.position, travelSpeed * Time.fixedDeltaTime);

            float distanceToEnemy = Vector3.Distance(transform.position, _target.position);
            if (distanceToEnemy < 0.1f) {
                // TODO trigger vfx on hit
                _target.GetComponent<Enemy>().TakeDamage(damage);
                _target = null;
                gameObject.SetActive(false);
            }
        }
    }

    public void TargetEnemy(Transform _transform) {
        _target = _transform;
    }
}
