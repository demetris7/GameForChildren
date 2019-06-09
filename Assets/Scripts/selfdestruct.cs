using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfdestruct : MonoBehaviour
{
    public int destroyTimer = 4;

    void Start()
    {
        Destroy(this.gameObject, destroyTimer);
    }
}
