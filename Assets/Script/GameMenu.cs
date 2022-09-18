using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    // methods built for the two buttons
    public void OnStartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnExitGame()
    {
#if UNITY_EDITOR
        // if it is played in Unity, just stop the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
