// This is not a MonoBehavior class, it's just used to store the data for each turret
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class turretData
{
    public GameObject turretPrefab;
    public int cost;
    public GameObject upgradedPrefab;
    public int upgradeCost;
    public TurretType type;
}

public enum TurretType
{
    Laser,
    Missile,
    Standard
}