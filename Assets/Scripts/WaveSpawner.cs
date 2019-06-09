using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { spawning, waiting, counting };
    
    [System.Serializable]
    
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }
    public Wave[] waves;
    public int nextWave = 0;
    public int NextWave
    {
        get { return nextWave+1; }
    }
    public float timeBetweenwaves = 5f;
    public float waveCountDown;
    public float WaveCountDown
    {
        get { return waveCountDown; }
    }
    private SpawnState state = SpawnState.counting;
    public float searchCountdown;
    public Transform[] spawnpoints;
    public SpawnState State
    {
        get { return state; }
    }
    
    void Start()
    {
        waves[0].name = "bob";
        waves[0].rate = 2;
        waves[0].count = 3;
        waves[1].name = "boss";
        waves[1].rate = 2;
        waves[1].count = 1;
        searchCountdown = 1f;
        waveCountDown = timeBetweenwaves;    
    }
  void Update()
    {
        if (state == SpawnState.waiting)
        {
            Debug.Log("wating");
            if (!EnemyIsAlive())
            {
                Debug.Log("wave completed");
          
                beginnewRound();
              
            }
            else
            {
                return;
            }
        }
       if(waveCountDown<0)
        {
            if (state != SpawnState.spawning)
            {
          
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }
    public bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }
    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("spawning wave");
        state = SpawnState.spawning;

        for(int i = 0; i <= _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        state = SpawnState.waiting;

        yield break;
    }
    void beginnewRound()
    {
        waves[0].count++;
       //wp.effectSpawnRate += 10;
        //wp.firerate += 10;
       // Debug.Log(wp.effectSpawnRate);
        Debug.Log("wave completed");
      
    state = SpawnState.counting;
        waveCountDown = timeBetweenwaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("all waves cmpoplete");
        }
        else
        {


            nextWave++;
        }
       
    }
        
    void SpawnEnemy(Transform _enemy)
    {
        Transform _sp = spawnpoints[Random.Range(0, spawnpoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
        Debug.Log("spawn enemy");   
    }
}
