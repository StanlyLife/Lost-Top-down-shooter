using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {



    [System.Serializable]
    public class Wave {
        public Enemy[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }


    public Wave[] waves;

    public Transform[] spawnPoints;
    public float timeBetweenWaves;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;

    public bool spawningFinished;

    [Header("Scene transition")]
    public SceneTransition transition;

    private void Start() {
        player = GameObject.FindWithTag("Player").transform;
        
    }

    private void Update() {

        if (spawningFinished == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
            spawningFinished = false;
            if (currentWaveIndex + 1 < waves.Length) {
                currentWaveIndex++;
                StartCoroutine(CallNextWave(currentWaveIndex));
            }else {
                print("Calling for store from WaveSpawner");
                GameObject.FindGameObjectWithTag("NEVERDIE").GetComponent<Inventory>().waveAttribute++;
                transition.LoadScene("store");
            }

        }



    }

    IEnumerator CallNextWave(int waveIndex) {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(waveIndex));
        GameObject.FindGameObjectWithTag("NEVERDIE").GetComponent<Inventory>().waveAttributeX5++;

    }

    IEnumerator SpawnWave(int waveIndex) {
        currentWave = waves[waveIndex];

        for (int i = 0; i < currentWave.count; i++) {

            if (player == null) {
                yield break;
            }
            Enemy randomEnemy = currentWave.enemies[UnityEngine.Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            try {
                Instantiate(randomEnemy, randomSpawnPoint.position, transform.rotation);
            }catch (Exception e){ Debug.Log("ERROR: WaveSpawner unable to instantiate enemy: " + e); }

            if (i == currentWave.count - 1) {
                spawningFinished = true;
            } else {
                spawningFinished = false;
            }

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);

        }

    }

}