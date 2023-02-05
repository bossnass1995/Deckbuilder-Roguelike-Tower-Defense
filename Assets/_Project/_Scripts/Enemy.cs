using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float EnemySpeed = 60f;
    private float DistTraveledPerTimestep => EnemySpeed * Time.fixedDeltaTime;
    public float TotalDistanceTraveled = 0f;

    private Transform previousWaypoint;
    private Transform nextWaypoint;
    private int waypointIndex;
    
    [SerializeField] public float startingHealth = 2f;
    private float currentHealth;

    void OnEnable()
    {
        // Movement
        previousWaypoint = Waypoints.points[0];
        nextWaypoint = Waypoints.points[1];

        // Health
        currentHealth = startingHealth;
    }

    void FixedUpdate()
    {
        Vector3 vecToNextWaypoint = nextWaypoint.position - transform.position;
        Vector3 moveDirection = vecToNextWaypoint.normalized;
        float distToNextWaypoint = Vector3.Distance(transform.position, nextWaypoint.position);
        Vector3 vectorToMove;
        if (distToNextWaypoint <= DistTraveledPerTimestep) {
            vectorToMove = vecToNextWaypoint;
            EnterNextSegment();
        } else {
            vectorToMove = moveDirection * EnemySpeed * Time.fixedDeltaTime;
        }
        // (MAYBE) perform distance calculation so we don't move less than the correct enemy speed
        // for cycles before corners

        transform.Translate(vectorToMove, Space.World);
        TotalDistanceTraveled += vectorToMove.magnitude;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            TakeDamage(1f);
        }
    }

    void EnterNextSegment() {
        previousWaypoint = Waypoints.points[waypointIndex];
        if (waypointIndex < Waypoints.points.Length - 1) {
            waypointIndex++;
        }
        nextWaypoint = Waypoints.points[waypointIndex];
    }

    public void TakeDamage(float damage) {
        currentHealth = currentHealth - damage;

        if (currentHealth <= 0f) {
            ResetEnemy();
            gameObject.SetActive(false);
        }
    }

    void ResetEnemy() {
        currentHealth = startingHealth;
        previousWaypoint = Waypoints.points[0];
        nextWaypoint = Waypoints.points[1];
        TotalDistanceTraveled = 0f;
        waypointIndex = 0;
    }
}
