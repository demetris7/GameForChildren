using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAi : MonoBehaviour
{
    public Transform target;
    public float updateRate = 2f;

    private Seeker seeker;
    private Rigidbody2D rb;

    public Path path;

    public float speed = 300;
    public ForceMode2D fMode;
    private int currentwaypoint = 0;
    [HideInInspector]
    public bool pathISEnded = false;

    public float nextWaypointdist = 3;
    private bool searchingforplayer=false;
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            if (!searchingforplayer)
            {
                searchingforplayer = true;
                StartCoroutine(SearchForPlaya());
            }
            return;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        StartCoroutine(UpdatePath());
        
    }
    IEnumerator SearchForPlaya()
    {
        GameObject res= GameObject.FindGameObjectWithTag("Player");
        if (res == null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SearchForPlaya());
        }
        else
        {
            target = res.transform;
            searchingforplayer = false;
            StartCoroutine(UpdatePath());
            yield return false;
        }
    }
    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            if (!searchingforplayer)
            {
                searchingforplayer = true;
                StartCoroutine(SearchForPlaya());
            }
            yield return false;
        }

        Debug.Log("trying to find him");
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath()); 
    } 
   public void OnPathComplete(Path p)
    {
        Debug.Log("we got a path.did it have an error?" + p.error);
        if (!p.error)
        {
            path = p;
            currentwaypoint = 0;
        }
    }

     void FixedUpdate()
    {
        if (target == null)
        {
            if (!searchingforplayer)
            {
                searchingforplayer = true;
                StartCoroutine(SearchForPlaya());
            }
            return;
        }


        if (path == null)
        {
            //Debug.Log("hery");
            return;
        }
        if (currentwaypoint >= path.vectorPath.Count)
        {
            if (pathISEnded)
            {
                return;
            }
            Debug.Log("end of path reached");
            pathISEnded = true;
            return;
        }
        pathISEnded = false;
        Vector3 dir = (path.vectorPath[currentwaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        rb.AddForce(dir,fMode);
        if ((Vector3.Distance(transform.position, path.vectorPath[currentwaypoint])) < nextWaypointdist)
        {
            currentwaypoint++;
            return;
        }
    }
}
