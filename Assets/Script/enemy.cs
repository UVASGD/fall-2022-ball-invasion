using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{

    public int movingSpeed = 10;
    private int originalSpeed;
    // the road points that enemies follow
    private Transform[] positions;
    public int idx = 0;

    public float totalHp = 150;
    private float hp;
    public int onDieAward = 100;

    public GameObject explosionEffectPrefab;
    public GameObject addMoneyEffectPrefab;

    public Slider hpSlider;

    private Transform tempTransform;

    public float slowCountDown = 0;
    public float freezeCountDown = 0;


    // Start is called before the first frame update
    void Start()
    {
        hp = totalHp;
        positions = wayPoints.positions;
        tempTransform = transform.Find("temp").GetComponent<Transform>();
        tempTransform.LookAt(positions[idx]);
        addMoneyEffectPrefab.transform.Find("Canvas").Find("MoneyText").GetComponent<Text>().text = "+$" + onDieAward;
        originalSpeed = movingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(freezeCountDown > 0)
        {
            movingSpeed = 0;
            freezeCountDown -= Time.deltaTime;
        }
        else if(slowCountDown > 0)
        {
            movingSpeed = (int)Mathf.Ceil(originalSpeed * 0.7f);
            slowCountDown -= Time.deltaTime;
        }
        else
        {
            movingSpeed = originalSpeed;
        }
        Move();

        freezeCountDown = Mathf.Max(freezeCountDown, 0);
        slowCountDown = Mathf.Max(slowCountDown, 0);
    }

    void Move()
    {
        // when enemy reach the end of the road
        if (idx >= positions.Length)
        {
            ReachDestination();
            return;
        }
        // move toward the next waypoint
        transform.Translate((positions[idx].position - transform.position).normalized * Time.deltaTime * movingSpeed);
        if (Vector3.Distance(positions[idx].position, transform.position) < 0.4f)
        {
            idx++;
            if (idx < positions.Length)
            {
                tempTransform.LookAt(positions[idx]);
            }
        }
    }

    void ReachDestination()
    {
        // instantly lose the game if an enemy reaches the destination
        gameManager.Instance.Fail();
        GameObject.Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        enemySpawner.enemyAlive--;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        hpSlider.value = hp / totalHp;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // die and generate dying effect
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        GameObject addMoney = GameObject.Instantiate(addMoneyEffectPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        Destroy(effect, 1.5f);
        Destroy(addMoney, 1.5f);
        buildManager.instance.updateMoney(onDieAward);
    }
}
