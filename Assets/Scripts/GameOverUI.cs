using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
   
    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private GameObject HigscoreUI;
    public void Quit()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }
    public void Retry()
    {
        Debug.Log("Application Quit");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }   
    public void Score()
    {
        gameOverUI.SetActive(false);
        HigscoreUI.SetActive(true);
       
    }
}
