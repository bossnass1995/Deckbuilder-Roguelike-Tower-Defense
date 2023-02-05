using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _deathParticles;
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
      if (nextWaypoint != null) {
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
