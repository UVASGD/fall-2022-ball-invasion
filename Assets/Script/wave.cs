// This is not a MonoBehavior class, it is used to store information about each wave
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// the enemy property for every wave
public class wave
{
    public GameObject enemyPrefab;
    // number of enemies to generate
    public int count;
    // seconds to wait before generating a new enemy
    public float rate;

}
