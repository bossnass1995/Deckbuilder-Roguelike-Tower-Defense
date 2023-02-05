using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] public Transform spawnPoint;
    [SerializeField] private ObjectPool babyCthuluPool;
    [SerializeField] private ObjectPool bigCthuluPool;

    private int numberOfEnemiesInWave = 0;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            numberOfEnemiesInWave++;
            StartCoroutine(SpawnWave(numberOfEnemiesInWave, babyCthuluPool));
        }
        if (Input.GetMouseButtonDown(1)) {
            StartCoroutine(SpawnWave(1, bigCthuluPool));
        }
    }

    IEnumerator SpawnWave(int numberOfEnemiesInWave, ObjectPool enemyPool) {
        for (int i = 0; i < numberOfEnemiesInWave; i++) {
            GameObject newEnemy = enemyPool.GetObjectFromPool();
            newEnemy.transform.position = spawnPoint.position;
            newEnemy.SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
    }

    
}
