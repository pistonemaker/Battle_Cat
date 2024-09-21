using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Data/Database")]
public class Database : ScriptableObject
{
    public List<CatData> cats;
    public List<MonsterData> monsters;
}

[Serializable]
public class CatData
{
    public int id;
    public Entity cat;
    public float waitTime;
    public int summonCost;
    public bool hasOwn;
    public bool isUsed;
}

[Serializable]
public class MonsterData
{
    public int id;
    public Entity monster;
}
