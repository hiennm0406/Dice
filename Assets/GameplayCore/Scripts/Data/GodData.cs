using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GodData", menuName = "Data/GodData", order = 1)]
public class GodData : SingletonScriptableObject<GodData>
{
    public List<God> ListGod = new List<God>();

    public God GetGod(int id)
    {
        foreach (var item in ListGod)
        {
            if (item.GodId == id)
            {
                return item;
            }
        }
        return null;
    }
}

[System.Serializable]
public class God
{
    public int GodId;
    public string GodName;
    public Stat BaseStat;
}