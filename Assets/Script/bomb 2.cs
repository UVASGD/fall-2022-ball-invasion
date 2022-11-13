using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bomb : MonoBehaviour
{
    private float t;
    public TextMeshPro number;
    public GameObject explosionEffectPrefab;
    // Start is called before the first frame update
    void Start()
    {
        t = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float timeAlive = Time.time - t;
        if(timeAlive > 3)
        {
            GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
            Destroy(this.gameObject);
            Destroy(effect, 0.55f);
        }
        else
        {
            number.text = (3 - (int)(timeAlive)).ToString();
        }
    }
}
