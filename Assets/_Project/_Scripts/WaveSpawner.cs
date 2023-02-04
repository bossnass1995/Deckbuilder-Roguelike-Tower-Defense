using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    [SerializeField] public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    private int waveIndex = 0;
    private int waveSpacingCount = 1;

    void Update() {
        if (countdown <= 0f) {
            StartCoroutine(SpawnWave(waveSpacingCount));
            countdown = timeBetweenWaves;
            waveSpacingCount++;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave(int waveSpacingCount) {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++) {
            SpawnEnemy();
            yield return new WaitForSeconds(0.3f);
        }
    }

    void SpawnEnemy() {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
