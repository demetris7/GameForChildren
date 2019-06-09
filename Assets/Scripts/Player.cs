using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int Maxhealth = 100;
        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, Maxhealth); }
        }
        public void Init()
        {
            curHealth = Maxhealth;
        }

    }

    public PlayerStats playstats = new PlayerStats();
    public int fallboundary = -20;
    void Update()
    {
        if (transform.position.y <= fallboundary)
        {
            DamagePlayer(999999);
        }
    }
    [SerializeField]
    private Statusind statusindicator;

    void Start()
    {
        playstats.Init();
        if (statusindicator == null)
        {
            Debug.LogError("No status indicator on player");
        }
        else
        {
            statusindicator.SetHealth(playstats.curHealth, playstats.Maxhealth);
        }
    }
    public void DamagePlayer(int damage)
    {
        playstats.curHealth -= damage;
        if (playstats.curHealth <= 0)
        {
            GameMaster.KillPlaya(this);
        }
        statusindicator.SetHealth(playstats.curHealth, playstats.Maxhealth);

    }
}