using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : Singleton<GameManager>
{
    bool pause=false;
    bool main = true;
    public GameObject pauseMenu;
    public GameObject mainMenu;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if (main)
            return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
            {
                Resume();
            }
            else
                Pause();
        }   
    }

    public void Play()
    {
        main = false;
        EventSystem.current.enabled = true;
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        LevelController.instance.AwakePlayer();
    }
    public void Pause()
    {
        LevelController.instance.SleepPlayer();
        
        AudioListener.pause = true;
        pause = true;
        pauseMenu.SetActive(true);
        //Time.timeScale = 0.0f;
    }
    public void Resume()
    {
        LevelController.instance.AwakePlayer();
        
        AudioListener.pause = false;
       // Time.timeScale = 1.0f;
        pause = false;
        pauseMenu.SetActive(false);
    }
    public void ExitToMenu()
    {
        main = true;
        LevelController.instance.SleepPlayer();
        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}


