using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    private Text Scoretext;
    public int highscore;
    
    // Start is called before the first frame update
    void Start()
    {
        Scoretext = GetComponent<Text>();
        highscore = PlayerPrefs.GetInt("highscore", highscore);
    }

    // Update is called once per frame
    void Update()
    {
        Scoretext.text = "score: " + GameMaster.Score.ToString();
        Debug.Log(GameMaster.Score);
        Debug.Log("highscore" + highscore);
        if (GameMaster.Score > highscore)
        {
            highscore = GameMaster.Score;

            Debug.Log("highscore"+highscore);
            PlayerPrefs.SetInt("highscore", highscore);
        }
    }
}
