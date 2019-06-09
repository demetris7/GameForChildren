﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralaxing : MonoBehaviour
{
    public Transform[] backgrounds;
    private float[] parallaxscales;
    public float smoothing=1f;

    public Transform cam;
    private Vector3 previousCamPos;

     void Awake()
    {
        cam = Camera.main.transform;    
    }
    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position;
        parallaxscales = new float[backgrounds.Length];

        for(int i = 0; i < backgrounds.Length; i++)
        {
            parallaxscales[i] = backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float paralax = (previousCamPos.x - cam.position.x) * parallaxscales[i];

            float backgroundTargetPosX = backgrounds[i].position.x + paralax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }
        previousCamPos = cam.position;
    }
}
