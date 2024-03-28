using Sirenix.OdinInspector;
using System;
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

    private void OnValidate()
    {
        foreach (var item in ListGod)
        {
            while (item.dice.Count < 5)
            {
                item.dice.Add(0);
            }
            while (item.dice.Count > 5)
            {
                item.dice.RemoveAt(5);
            }
        }
    }
}

[System.Serializable]
public class God
{
    public int GodId;
    public string GodName;
    public Stat BaseStat;
    public Stat BaseStatIncrease;
    [ValueDropdown("GetList")]
    public List<int> dice; // 1-3 là cố định, 4 5 có thể đổi được

    public IEnumerable GetList()
    {
        ValueDropdownList<int> result = new ValueDropdownList<int>();
        foreach (var item in DiceData.instance.listDice)
        {
            result.Add(item.DiceName, item.DiceId);
        }
        return result;
    }
}

public class GodInfo
{
    public int GodId;
    public int Level;
    public Stat GodStat;

    public GodInfo(int id, int level)
    {
        GodId = id;
        Level = level;
        God _base = GodData.instance.GetGod(id);
        GodStat = new Stat();
        GodStat.Power = _base.BaseStat.Power + Level * _base.BaseStatIncrease.Power;
        GodStat.HP = _base.BaseStat.HP + Level * _base.BaseStatIncrease.HP;
        GodStat.Luck = _base.BaseStat.Luck;
        GodStat.CritDmg = _base.BaseStat.CritDmg;
        GodStat.CritRate = _base.BaseStat.CritRate;
    }
}