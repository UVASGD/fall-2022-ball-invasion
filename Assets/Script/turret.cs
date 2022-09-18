using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{
    // keep a list of enemies in the attack region
    private List<GameObject> enemies = new List<GameObject>();

    // controller of the attack rate
    public float attackRate = 1;
    private float timer = 0;

    // information for corresponding bullet
    public GameObject bulletPrefab;
    public Transform firePosition;

    public Transform head;

    // special for Laser
    public bool useLaser = false;
    public float damageRate = 10;
    public LineRenderer laser;
    public GameObject laserEffect;

    private void Start()
    {
        // to be able to attack once it is built
        timer = attackRate;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if a enemy enters the attack region, add it to the enemy list
        if (other.tag == "Enemy")
        {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // if a enemy leaves the attack region, remove it from the enemy list
        if (other.tag == "Enemy")
        {
            enemies.Remove(other.gameObject);
        }
    }

    private void Update()
    {
        if (!useLaser)
        {
            // attack of non-laser turrets
            timer += Time.deltaTime;
            if (enemies.Count > 0 && timer >= attackRate)
            {
                timer = 0;
                Attack();
            }
        }
        else
        {
            // special for laser turret
            if (enemies.Count > 0)
            {
                if (!laser.enabled)
                {
                    laser.enabled = true;
                    laserEffect.SetActive(true);
                }
                LaserAttack();
            }
            else
            {
                laser.enabled = false;
                laserEffect.SetActive(false);
            }
        }

        // make the turret look at the targeted enemy
        if (enemies.Count > 0 && enemies[0] != null)
        {
            Vector3 targetPosition = enemies[0].transform.position;
            // we don't want our turret to move up or down
            targetPosition.y = transform.position.y;
            head.LookAt(targetPosition);
        }
    }

    private void Attack()
    {
        if (enemies[0] == null)
        {
            // if the targeted enemy becomes null (died or leaves the attack region)
            UpdateEnemies();
        }
        if (enemies.Count > 0)
        {
            // create bullet
            GameObject Bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            Bullet.GetComponent<bullet>().SetTarget(enemies[0].transform);
        }
    }

    private void LaserAttack()
    {
        if (enemies[0] == null)
        {
            // if the targeted enemy becomes null (died or leaves the attack region)
            UpdateEnemies();
        }
        if (enemies.Count > 0)
        {
            // set two ends of the laser to turret and enemy
            laser.SetPositions(new Vector3[] {firePosition.position, enemies[0].transform.position});
            enemies[0].GetComponent<enemy>().TakeDamage(damageRate * Time.deltaTime);
            // use laser effect
            laserEffect.transform.position = enemies[0].transform.position;
            Vector3 pos = transform.position;
            pos.y = enemies[0].transform.position.y;
            laserEffect.transform.LookAt(pos);
        }
    }

    private void UpdateEnemies()
    {
        List<int> emptyIndex = new List<int>();
        // function same as "enemies.RemoveAll(null);" (but it is not viable to just write that code)
        for (int i=0; i<enemies.Count; i++)
        { 
            if (enemies[i] == null)
            {
                emptyIndex.Add(i);
            }
        }

        for (int i=0; i<emptyIndex.Count; i++)
        {
            enemies.RemoveAt(emptyIndex[i] - i);
        }
    }
}
