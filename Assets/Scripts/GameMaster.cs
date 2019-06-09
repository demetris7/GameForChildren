using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    
    [SerializeField]
    private int MaxLives = 3;
    private static int _remaininglives;
    private static int _score;
    public static int Score
    {
        get { return _score; }
    }
    public static int RemainingLives
    {
        get { return _remaininglives; }
    }

    public static GameMaster gm;
    void Start()
    {
        _score = 0;
        _remaininglives = MaxLives;
    }   
    void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }
    public Transform playerprefab;
    public Transform spawnppOINT;
    public int SpawnDelay=2;

    [SerializeField]
    private GameObject gameOverUI;
    public void Endgame()
    {
        Debug.Log("game over");
        gameOverUI.SetActive(true);
    }

    
    public IEnumerator RespawnPlaya()
    {
        yield return new WaitForSeconds(SpawnDelay);
        Instantiate(playerprefab, spawnppOINT.position, spawnppOINT.rotation);
        
    }
    public static void KillPlaya(Player playa)
    {
        Destroy(playa.gameObject);
        _remaininglives -= 1;
        if (_remaininglives <= 0)
        {
            gm.Endgame();
        }
        else
        {
            gm.StartCoroutine(gm.RespawnPlaya());
        }
        
    }
    public static void KillEnemy(Enemy enemy)
    {
        _score += 5;
        gm._Killenemy(enemy);
    }
    public void _Killenemy(Enemy _enemy)
    {
      Instantiate(_enemy.enemydeathparticle, _enemy.transform.position, Quaternion.identity);
        Destroy(_enemy.gameObject);
    }
}
