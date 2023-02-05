using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Bullet
{
    [SerializeField] private float travelSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    
    private Transform spawnLocation;
    
    void FixedUpdate()
    {
        if (_target != null) {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, travelSpeed * Time.fixedDeltaTime);
            Vector3 travelDirection = (_target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(Vector3.back, travelDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.fixedDeltaTime);

            float distanceToEnemy = Vector3.Distance(transform.position, _target.position);
            if (distanceToEnemy < 0.1f) {
                // TODO trigger vfx on hit
                _target.GetComponent<Enemy>().TakeDamage(damage, bossKillingMultiplier);
                _target = null;
                gameObject.SetActive(false);
            }
        }
    }
}
