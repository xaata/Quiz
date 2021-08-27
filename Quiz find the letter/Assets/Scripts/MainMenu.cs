using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject levelCompleteMenu;
    [SerializeField]
    private GameObject gameOverMenu;

    private bool gameOver;

    
  
    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            gameOverMenu.SetActive(true);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
        //-------Баг при рестарте не ставится тавер(фикс есть где то в гайде)
        //Hover.Instance.Deactivate();
        //DropTower();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    public void LevelComplete()
    {
        Time.timeScale = 0;
        levelCompleteMenu.SetActive(true);
    }
    public void ShowPauseMenu()
    {
        //if(LevelCompleteMenu.activeSelf || GameOverMenu.activeSelf)
        if (optionsMenu.activeSelf)
        {
            optionsMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            if (!pauseMenu.activeSelf)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
    }
    public void ShowOptions()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}
