using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING
    };

    [System.Serializable]    
    public class Wave
    {
        public String name;
        public Transform enemy;
        public int count;
        public float rate;
    }
        public Wave[] waves;
        public Transform[] spawnPoints;
        private int nextWave = 0;

        public float timeBetweenWaves = 5f;
        private float waveCountdown;
        private float searchCountdown = 1f;
        private SpawnState state = SpawnState.COUNTING;
        
    void Start()
    {
        waveCountdown = timeBetweenWaves; // To start the countdown, set differently if you want the time before the first wave to be different than the time between waves
    }

    // Update is called once per frame
    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyisAlive())
            {
                Debug.Log("Wave Completed!");
                return;
                
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave])); //Start Spawning Wave
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if (nextWave + 1 > waves.Length - 1)
        {
            // Game Completed, Game Winning Scene!
            nextWave = 0; //Delete this for final
            Debug.Log("All Waves Complete!");
        }

        nextWave++;
    }
    bool EnemyisAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null) //Check for Enemy
            {
                return false;
            }
        }
        return true;
    }
   

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave :"+ _wave.name);
        state = SpawnState.SPAWNING;
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return  new WaitForSeconds(1f/_wave.rate); // Wait for the time between spawning enemies
        }
        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Transform _sp = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)]; // Picks one of the spawn points randomly to spawn enemy
        Instantiate(_enemy, _sp.position, _sp.rotation);
        Debug.Log("Spawning Enemy:"+_enemy.name);
    }
}
