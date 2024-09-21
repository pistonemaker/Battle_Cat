using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Money Data", menuName = "Data/Money Data")]
public class MoneyData : ScriptableObject
{
    public List<MoneyLevel> moneyLevel;
}

[Serializable]
public class MoneyLevel
{
    public List<MiniMoneyLevel> miniMoneyLevels;
}

[Serializable]
public class MiniMoneyLevel
{
    public bool canLevelUp;
    public float timeToUp1Money;
    public int moneyToLevelUp;
    public int maxMoney;
}