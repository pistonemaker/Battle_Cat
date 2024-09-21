using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Territory", menuName = "Data/Territory Data")]
public class TerritoryUnitsData : ScriptableObject
{
    public List<TerritoryData> territoryUnitsData;
}

[Serializable]
public class TerritoryData
{
    public bool isClear;
    public int clearCount;
    public string territoryName;
    public int energyCost;
}