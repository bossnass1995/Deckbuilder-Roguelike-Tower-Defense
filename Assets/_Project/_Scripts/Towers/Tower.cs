using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour 
{
    [SerializeField] bool targetFarthestTraveledEnemy = true; // if false, targets nearest enemy to tower

    // All towers
    [SerializeField] private float _range;
    [SerializeField] private float fireRate = 1f; // in bullets per second
    private float secBetweenShots => 1f / fireRate;

    // Ballista
    [SerializeField] private GameObject partToSwivel;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private Transform bulletSpawnLocation;

    private bool bulletFired = false;
    private float fireCountdown;
    private Transform _target;
    private string enemyTag = "Enemy";

    private void OnEnable() {
        fireCountdown = secBetweenShots;
    }

    private void FixedUpdate() {
        UpdateTarget();
        fireCountdown -= Time.deltaTime;
        
        if (fireCountdown <= 0f && _target != null && !bulletFired) {
            fireCountdown = secBetweenShots;
            GameObject bulletObj = bulletPool.GetObjectFromPool();
            bulletObj.transform.position = bulletSpawnLocation.position;
            bulletObj.transform.rotation = bulletSpawnLocation.rotation;
            bulletObj.GetComponent<Bullet>().TargetEnemy(_target);
        }
    }

    private void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistanceToTower = Mathf.Infinity;
        GameObject enemyToTarget = null;
        float farthestDistanceTraveled = 0f;

        foreach (GameObject enemy in enemies) {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < _range) {
                if (targetFarthestTraveledEnemy) {
                    float distanceTraveled = enemy.GetComponent<Enemy>().TotalDistanceTraveled;
                    if (distanceTraveled > farthestDistanceTraveled) {
                        farthestDistanceTraveled = distanceTraveled;
                        enemyToTarget = enemy;
                    }
                } else {
                    if (distanceToEnemy < shortestDistanceToTower) {
                        shortestDistanceToTower = distanceToEnemy;
                        enemyToTarget = enemy;
                    }
                }
            }
        }

        if (enemyToTarget != null) {
            _target = enemyToTarget.transform;
            RotateTowards(_target);
        }
        else {
            _target = null;
        }
    }
    
    public void RotateTowards(Transform target) {
        Vector2 direction = (target.position - partToSwivel.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        partToSwivel.transform.rotation = Quaternion.Slerp(partToSwivel.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}