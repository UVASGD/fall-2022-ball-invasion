using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public int damage = 50;
    public float speed = 50;

    private Transform target;

    // the explosion effect of the bullet when it hits enemy
    public GameObject explosionEffectPrefab;

    public float explosionDistance = 1.2f;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        // when the enemy is killed while bullet is flying, just explode
        if (target == null)
        {
            Explode();
            return;
        }

        // make bullet track enemy
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // explode when hit enemy
        Vector3 dir = transform.position - target.position;
        if(dir.magnitude <= explosionDistance)
        {
            target.GetComponent<enemy>().TakeDamage(damage);
            Explode();
        }
    }

    private void Explode()
    {
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
        Destroy(effect, 1);
    }
}
