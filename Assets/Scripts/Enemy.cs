using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {
        public int Maxhealth = 100;
        private int _curHealth;
        public float startPcHealth = 1f;
        public int curTHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value,0,Maxhealth); }
        }
        public int damage=20;
       
        public void Init()
        {
            curTHealth = Maxhealth;
        }

    }
    
    public EnemyStats  stats= new EnemyStats();
    public Transform enemydeathparticle;
    [Header("Optional: ")]
    [SerializeField]
    private Statusind statusIndicator;


    void Start()
    {
        
        stats.Init();
        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curTHealth, stats.Maxhealth);
        }
        if (enemydeathparticle == null)
        {
            Debug.LogError("no death particles referenced on enemy");
        }
    }
   // public EnemyStats stats = new EnemyStats();
    public int fallboundary = -20;
    /*void Update()
    {
        if (transform.position.y <= fallboundary)
        {
            DamagePlayer(999999);
        }
    }*/
    public void DamageEnemy(int damage)
    {
        stats.curTHealth-= damage;
        if (stats.curTHealth <= 0)
        {
            GameMaster.KillEnemy(this);
        }
        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curTHealth, stats.Maxhealth);
        }
    }
     void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("on collison");
        Player player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            player.DamagePlayer(stats.damage);
            DamageEnemy(99999);
        }
    }
}
