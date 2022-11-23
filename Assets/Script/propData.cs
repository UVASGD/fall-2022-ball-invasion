// Stores data of the props
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class propData
{
    public GameObject propPrefab;
    public int cost;
    public PropType type;
}

public enum PropType
{
    FreezingGernade,
    Bomb
}
