using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Transform _target;
    private Enemy _targetEnemy;
    
    private string enemyTag = "Enemy";
    [SerializeField] private float _range;
    [SerializeField] private GameObject partToSwivel;
    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        
    }

    private void Update()
    {
        UpdateTarget();
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= _range)
        {
            _target = nearestEnemy.transform;
            _targetEnemy = nearestEnemy.GetComponent<Enemy>();
            RotateTowards(_target);
        }
        else
            _target = null;
    }
    
    public void RotateTowards(Transform target) {
        Vector2 direction = (target.position - partToSwivel.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        partToSwivel.transform.rotation = Quaternion.Slerp(partToSwivel.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }


}