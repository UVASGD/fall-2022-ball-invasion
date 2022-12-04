using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float generateWaitTime = 15;
    private float generateEnemyCountDown;

    public List<GameObject> smallEnemies;
    private int enemyId = 0;

    private void Start()
    {
        generateEnemyCountDown = generateWaitTime/2;
    }

    private void Update()
    {
        if(transform.GetComponent<enemy>().freezeCountDown <= 0)
        {
            generateEnemyCountDown -= Time.deltaTime;
        }
        if(generateEnemyCountDown <= 0)
        {
            generateEnemyCountDown = generateWaitTime;
            StartCoroutine(generate(smallEnemies[enemyId], 6-enemyId));
            enemyId = (enemyId + 1) % smallEnemies.Count;
        }
    }

    IEnumerator generate(GameObject enemyPrefab, int k)
    {
        for(int i=0; i<k; i++)
        {
            GameObject e = GameObject.Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            e.GetComponent<enemy>().idx = transform.GetComponent<enemy>().idx;
            enemySpawner.enemyAlive++;
            yield return new WaitForSeconds(1);
        }
    }
}


