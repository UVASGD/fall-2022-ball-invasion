using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicContinuerGame : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] menuM = GameObject.FindGameObjectsWithTag("MenuMusic");
        GameObject[] music = GameObject.FindGameObjectsWithTag("Music");

        if(music.Length > 1)
        {
            Destroy(this.gameObject);
        }

        foreach(GameObject m in menuM)
        {
            Destroy(m.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
