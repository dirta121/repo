using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    bool pause=false;
    public GameObject gamePauseMenu;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
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

    public void StartGame() { }
    public void Pause()
    {
        AudioListener.pause = true;
        pause = true;
        gamePauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void Resume()
    {
        AudioListener.pause = false;
        Time.timeScale = 1.0f;
        pause = false;
        gamePauseMenu.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }


    public enum GameStatus
    {
        Pause,
        Start
    }
}


