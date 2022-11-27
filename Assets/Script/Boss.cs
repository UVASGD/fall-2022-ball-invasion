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
        generateEnemyCountDown = generateWaitTime;
    }

    private void Update()
    {
        generateEnemyCountDown -= Time.deltaTime;
        if(generateEnemyCountDown <= 0)
        {
            generateEnemyCountDown = generateWaitTime;
            StartCoroutine(generate(smallEnemies[enemyId]));
            enemyId = (enemyId + 1) % smallEnemies.Count;
        }
    }

    IEnumerator generate(GameObject enemyPrefab)
    {
        for(int i=0; i<3; i++)
        {
            GameObject e = GameObject.Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            e.GetComponent<enemy>().idx = transform.GetComponent<enemy>().idx;
            yield return new WaitForSeconds(1);
        }
    }
}


