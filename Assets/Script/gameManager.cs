// This class controlls the UI of the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    
    public GameObject endUI;
    // "You win" if won, "Game Over" if failed
    public Text endText;

    public static gameManager Instance;

    private enemySpawner spawner;

    private void Start()
    {
        Instance = this;
        spawner = GetComponent<enemySpawner>();
    }

    public void Win()
    {
        endUI.SetActive(true);
        endText.text = "YOU WIN";
        endText.color = Color.yellow;
    }

    public void Fail()
    {
        // stop generating enemies after failing the game
        spawner.Stop();
        endUI.SetActive(true);
        endText.text = "GAME OVER";
        endText.color = Color.white;
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
}
