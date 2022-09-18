// This is the class that controlls enemy generation
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    public static int enemyAlive = 0;

    // add waves manually in Unity, so we can keep different scenes have different waves
    public wave[] waves;

    // the starting point of enemies
    public Transform START;

    // this is the seconds to wait after a wave is cleared
    public float waveRate = 1;
    private Coroutine coroutine;

    // start or end generating enemies
    private void Start()
    {
        coroutine = StartCoroutine(SpawnEnemy());
    }
    public void Stop()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator SpawnEnemy()
    {
        foreach(wave Wave in waves){
            for(int i=0; i<Wave.count; i++)
            {
                // generate enemies in one wave
                GameObject.Instantiate(Wave.enemyPrefab, START.position, Quaternion.identity);
                enemyAlive++;
                yield return new WaitForSeconds(Wave.rate);
            }
            // while there are enemies alive, we don't want to generate new enemies
            while (enemyAlive > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }

        while (enemyAlive > 0)
        {
            yield return 0;
        }

        gameManager.Instance.Win();
    }
}
