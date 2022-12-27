using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;



public class endMenu : MonoBehaviour
    
{

    
    public GameObject PauseMenuScreen;


    public void pauseButton()
    {
        PauseMenuScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void KeepPlaying ()
    {
        PauseMenuScreen.SetActive(false);
        Time.timeScale = 1f;
    }

     public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        PauseMenuScreen.SetActive(false);
    }

    public void endGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        PauseMenuScreen.SetActive(false);
    }
    
    public void endGame2()
    { 
        Application.Quit();
    }
            
    

}
