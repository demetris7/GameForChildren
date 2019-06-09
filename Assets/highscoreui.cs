using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class highscoreui : MonoBehaviour
{
    private Text Scoretext;
    Score highsc;
    int highscore;
    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", highscore);
        highsc = new Score();
        Scoretext = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log(highsc.highscore + "yaya");
        Scoretext.text = "" + highscore.ToString();
      
    }
}
