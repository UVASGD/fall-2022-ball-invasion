using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneEnemy : MonoBehaviour
{
    public GameObject explosionEffectPrefab;
    public float beforeDie;

    // Update is called once per frame
    void Update()
    {
        beforeDie -= Time.deltaTime;
        if(beforeDie <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // die and generate dying effect
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
        Destroy(effect, 1.5f);
    }
}
