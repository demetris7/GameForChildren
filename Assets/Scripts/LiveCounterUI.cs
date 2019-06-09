using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class LiveCounterUI : MonoBehaviour
{
    
    private Text livestext;
    // Start is called before the first frame update
    void Start()
    {
        livestext = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        livestext.text = "lives: " + GameMaster.RemainingLives.ToString();
    }
}
