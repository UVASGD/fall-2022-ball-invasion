using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    public float waitTime = 0.2f;
    public float range = 10;
    public float freezeTime = 2;
    public float slowTime = 3;
    public float slowRate = 0.3f;

    public GameObject snowFlakeEffect;
    public GameObject explodeEffect;

    private Coroutine coroutine;

    // Start is called before the first frame update
    void Start()
    {
        coroutine = StartCoroutine(FreezeEnemy());
    }

    private void OnDestroy()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator FreezeEnemy()
    {
        Collider[] EnemyColliders = Physics.OverlapSphere(transform.position, range);
        List<enemy> Enemies = new List<enemy>();
        List<int> speeds = new List<int>();

        // Wait for 0.1 second and then produce effects
        yield return new WaitForSeconds(waitTime);
        Vector3 effectPosition = transform.position;
        effectPosition.y = 0;
        GameObject SnowFlake = GameObject.Instantiate(snowFlakeEffect, effectPosition, Quaternion.identity);
        SnowFlake.layer = LayerMask.NameToLayer("Ignore Raycast");
        GameObject Explosion = GameObject.Instantiate(explodeEffect, effectPosition, Quaternion.identity);
        this.transform.Find("Sphere").gameObject.SetActive(false);
        Destroy(Explosion, 1);

        // freeze enemies for freezeTime
        foreach (Collider Enemy in EnemyColliders)
        {
            if (Enemy.tag == "Enemy")
            {
                Enemies.Add(Enemy.GetComponent<enemy>());
                speeds.Add(Enemy.GetComponent<enemy>().movingSpeed);
                Enemy.GetComponent<enemy>().movingSpeed = 0;
            }
        }
        yield return new WaitForSeconds(freezeTime);
        
        // slow down enemies for slowTime
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].movingSpeed = (int)Mathf.Ceil(speeds[i] * (1 - slowRate));
        }
        yield return new WaitForSeconds(slowTime);

        // snowFlakeEffect disappear
        SnowFlake.GetComponent<Animator>().SetTrigger("Disappear");
        Destroy(SnowFlake, .5f);

        // resume normal speed
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].movingSpeed = speeds[i];
        }

        Destroy(gameObject, 1);
    }
}
