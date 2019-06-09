using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum SpawnState { spawning, waiting, counting };
    [HideInInspector]
    public float firerate = 5;
    public int damage = 10;
    public LayerMask whatToHit;
    public Transform bullettrailprefab;
    public Transform HitPrefab;
    float timeToFire = 0;
   [HideInInspector]
    public float effectSpawnRate=5;
    float timeToSpawn=0;
    float timetoadd =30;
    WaveSpawner wavesucces;
    public Transform muzzleprefab;
    Transform firePoint;
    private SpawnState state = SpawnState.counting;
    // Start is called before the first frame update
    public SpawnState State
    {
        get { return state; }
    }
    private void Awake()
    {
        firerate = 5;
        effectSpawnRate = 5;
        wavesucces = new WaveSpawner();
        firePoint = transform.Find("FirePoin");
        if (firePoint == null)
        {
            Debug.LogError("nofirepoint");
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if (firerate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                Debug.Log("the firerate is"+firerate);
                timeToFire = Time.time + 1 / firerate;
                Shoot();
            }
        }
        if (Time.time >= timetoadd)
        {
            firerate += 1;
            effectSpawnRate += 1;
            timetoadd = Time.time + 30;

        }
    }
    void Shoot()
    {
        Vector2 mousepos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firepointpos = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firepointpos, mousepos - firepointpos, 100, whatToHit);



        // Debug.DrawLine(firepointpos, (mousepos - firepointpos) * 100, Color.cyan);
        if (hit.collider != null)
        {
            //Debug.DrawLine(firepointpos, hit.point, Color.red);

            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                //Debug.Log("we hit" + hit.collider.name + "and did" + damage + "damage");
                enemy.DamageEnemy(damage);
            }
        }
        if (Time.time >= timeToSpawn)
        {
            Vector3 hitposition;
            Vector3 hitNormal;
            if (hit.collider == null)
            {
                hitposition = (mousepos - firepointpos) * 30;
                hitNormal = new Vector3(9999, 9999, 9999);
            }
            else
            {
                
                hitposition = hit.point;
                hitNormal = hit.normal;
            }

            Effect(hitposition,hitNormal);
            timeToSpawn = Time.time + 1 / effectSpawnRate;
        }

    }

    void Effect(Vector3 hitpos,Vector3 hitNor)
    {

        Transform trail = Instantiate(bullettrailprefab, firePoint.position, firePoint.rotation) as Transform;
        LineRenderer lr = trail.GetComponent<LineRenderer>();

        if (lr != null)
        {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hitpos);
        }
        if(hitNor!=new Vector3(9999, 9999, 9999))
        {
            Instantiate(HitPrefab,hitpos,Quaternion.FromToRotation(Vector3.right,hitNor));
            Destroy(HitPrefab.gameObject, 1f);
           
        }
        // Destroy(trail.gameObject, 0.02f);
        Transform clone = (Transform)Instantiate(muzzleprefab, firePoint.position, firePoint.rotation);
        clone.parent = firePoint;
        float siZE = Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(siZE, siZE, siZE);


        Destroy(clone.gameObject, 0.02f);
    }
}