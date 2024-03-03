using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWay
{
    public float PowerFactor;
    public float HpFactor;
    public List<int> unitsInWay = new List<int>();
}
