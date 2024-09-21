using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Laze Gun Data", menuName = "Data/Laze Gun Data")]
public class LazeGunData : ScriptableObject
{
    public List<int> damages;
    public List<float> ranges;
    public List<float> waitTimes;
}
