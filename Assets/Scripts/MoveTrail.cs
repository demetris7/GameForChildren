﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour
{
    public int moveSpeed = 1;
    // Update is called once per frame
    void Update()
    {
       // transform.Translate(Vector3.right *moveSpeed);
        Destroy(gameObject,1);
    }
}
