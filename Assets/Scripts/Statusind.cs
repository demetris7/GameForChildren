using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statusind : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthbarrect;
    [SerializeField]
    private Text text;

    private void Start()
    {
        if (healthbarrect == null)
        {
            Debug.LogError("No health bar obgject");

        }
        if (text == null)
        {
            Debug.LogError("No health text obgject");
        }
    }
    public void SetHealth(int _cur,int _max)
    {
        float value = (float)_cur / _max;
        healthbarrect.localScale = new Vector3(value, healthbarrect.localScale.y, healthbarrect.localScale.z);
        text.text = _cur + "/" + _max + " HP";
    }
}
