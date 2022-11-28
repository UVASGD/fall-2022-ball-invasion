using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    public float waitTime = 0.2f;
    public float range = 10;
    public float freezeTime = 2f;
    public float slowTime = 3f;

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
  
        // Wait for 0.1 second and then produce effects
        yield return new WaitForSeconds(waitTime);
        Vector3 effectPosition = transform.position;
        effectPosition.y = 0;
        GameObject SnowFlake = GameObject.Instantiate(snowFlakeEffect, effectPosition, Quaternion.identity);
        SnowFlake.layer = LayerMask.NameToLayer("Ignore Raycast");
        GameObject Explosion = GameObject.Instantiate(explodeEffect, effectPosition, Quaternion.identity);
        this.transform.Find("Sphere").gameObject.SetActive(false);
        Destroy(Explosion, 1);
        Collider[] EnemyColliders = Physics.OverlapSphere(transform.position, range);

        // freeze enemies for freezeTime and slow down enemies for slowTime
        foreach (Collider Enemy in EnemyColliders)
        {
            if (Enemy.tag == "Enemy")
            {
                Enemy.GetComponent<enemy>().freezeCountDown = freezeTime;
                Enemy.GetComponent<enemy>().slowCountDown = slowTime;
            }
        }

        yield return new WaitForSeconds(freezeTime + slowTime);
        // snowFlakeEffect disappear
        SnowFlake.GetComponent<Animator>().SetTrigger("Disappear");
        Destroy(SnowFlake, .5f);

        Destroy(gameObject, 1);
    }
}
