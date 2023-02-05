using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _deathParticles;
    [SerializeField] public float EnemySpeed = 60f;
    [SerializeField] public bool IsBoss = false;
    private float DistTraveledPerTimestep => EnemySpeed * Time.fixedDeltaTime;
    public float TotalDistanceTraveled = 0f;

    private Transform previousWaypoint;
    private Transform nextWaypoint;
    private int waypointIndex;
    private Waypoints waypoints;
    
    [SerializeField] public float startingHealth = 2f;
    private float currentHealth;

    void Awake() {
        waypoints = GameObject.Find("Waypoints").GetComponent<Waypoints>();
    }

    void OnEnable()
    {
        // Movement
        waypointIndex = 0;
        previousWaypoint = Waypoints.points[0];
        nextWaypoint = Waypoints.points[1];

        TotalDistanceTraveled = 0f;

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

        MaybeFlipSprite(vectorToMove);
        transform.Translate(vectorToMove, Space.World);
        TotalDistanceTraveled += vectorToMove.magnitude;
    }

    void EnterNextSegment() {
        previousWaypoint = Waypoints.points[waypointIndex];
        if (waypointIndex < Waypoints.points.Length - 1) {
            waypointIndex++;
        }
        nextWaypoint = Waypoints.points[waypointIndex];
    }

    public Vector3 GetFutureLocation(float lookaheadTime) {
        // Start with lookaheadTime seconds
        // Find next waypoint vector
        int lookaheadWaypointIndex = waypoints.FindNextWaypointIndex(nextWaypoint);
        Transform lookaheadWaypoint = Waypoints.points[lookaheadWaypointIndex];
        float distanceToNextWaypoint = Vector3.Distance(
            transform.position, 
            lookaheadWaypoint.position
        );
        float timeToNextWaypoint = distanceToNextWaypoint / EnemySpeed;
        // If lookahead time is greater than that time,
        while (lookaheadTime > timeToNextWaypoint) {
            // Subtract that time from lookahead time
            lookaheadTime -= timeToNextWaypoint;
            // Repeat for next waypoint
            distanceToNextWaypoint = Vector3.Distance(
                Waypoints.points[lookaheadWaypointIndex].position, 
                Waypoints.points[++lookaheadWaypointIndex].position
            );
            lookaheadWaypoint = Waypoints.points[++lookaheadWaypointIndex].position;
            timeToNextWaypoint = distanceToNextWaypoint / EnemySpeed;
        }
        
            // If lookahead time is smaller than that time,
            // Calculate vector location with remaining time
    }

    public void TakeDamage(float damage, float bossKillingMultiplier) {
        if (IsBoss) {
            damage = damage * bossKillingMultiplier;
        }
        currentHealth = currentHealth - damage;

        if (currentHealth <= 0f) {
            OnDeath();
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
        // If sprite is still flipped, revert to normal
        if(transform.GetChild(0).transform.localScale.x < 0) {
            transform.GetChild(0).transform.localScale = new Vector3(
                transform.GetChild(0).transform.localScale.x * -1f,
                transform.GetChild(0).transform.localScale.y,
                transform.GetChild(0).transform.localScale.z
            );
        }
    }

    void MaybeFlipSprite(Vector3 vectorToMove) {
        Transform enemyChild = transform.GetChild(0);
        Vector3 childScale = enemyChild.transform.localScale;
        if ((vectorToMove.x < 0 && childScale.x > 0) || 
            (vectorToMove.x > 0 && childScale.x < 0)) {
            enemyChild.transform.localScale = new Vector3(
                -childScale.x,
                childScale.y,
                childScale.z
            );
        }
    }

    void OnDeath() {
        //instantiate particles
        foreach (var particle in _deathParticles)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
        }
        
        //leave decal
        // DecalController decalController = GetComponent<DecalController>();
        // decalController.MakeDecal(transform);
    }
}
