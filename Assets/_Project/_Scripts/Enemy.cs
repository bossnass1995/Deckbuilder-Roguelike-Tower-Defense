using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float speed = 60f;

    private Transform previousWaypoint;
    private Transform nextWaypoint;
    private int waypointIndex;
    
    [SerializeField] public float startingHealth = 2f;
    private float currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        // Movement
        previousWaypoint = Waypoints.points[0];
        nextWaypoint = Waypoints.points[1];

        // Health
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementDirection = nextWaypoint.position - transform.position;
        transform.Translate(movementDirection.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, nextWaypoint.position) < 0.01f) {
            EnterNextSegment();
        }

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

    void TakeDamage(float damage) {
        currentHealth = currentHealth - damage;

        if (currentHealth <= 0f) {
            Destroy(gameObject);
        }
    }
}
