// This class controlls the UI of the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    
    public GameObject endUI;
    public GameObject pauseUI;
    public GameObject winUI;

    public static gameManager Instance;

    private enemySpawner spawner;

    private void Start()
    {
        Instance = this;
        spawner = GetComponent<enemySpawner>();
    }

    public void Win()
    {
        winUI.SetActive(true);
    }

    public void Fail()
    {
        // stop generating enemies after failing the game
        spawner.Stop();
        endUI.SetActive(true);
    }

    public void OnReplayButtonDown()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMenuButtonDown()
    {
        // load the first scene, which is the menu
        SceneManager.LoadScene(0);
    }


    public void OnPauseButtonDown()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void onResumeButtonDown()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void onNextButtonDown()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
