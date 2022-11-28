using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicContinuer : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] menuM = GameObject.FindGameObjectsWithTag("MenuMusic");
        GameObject[] music = GameObject.FindGameObjectsWithTag("Music");

        if(menuM.Length > 1)
        {
            Destroy(this.gameObject);
        }

        foreach(GameObject m in music)
        {
            Destroy(m.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
