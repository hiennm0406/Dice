using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodManager : UnitBase
{
    public int GodId;
    public int Level;
    public God godData;

    private void Start()
    {
        BattleManager.Instance.godManager = this;
    }

    public void InitGod(int id, int level)
    {
        GodId = id;
        Level = level;
        godData = GodData.instance.GetGod(GodId);
        stat = new Stat();
        stat.CopyStat(godData.BaseStat);
        stat.Power = godData.BaseStat.Power + Level * godData.BaseStatIncrease.Power;
        stat.HP = godData.BaseStat.HP + Level * godData.BaseStatIncrease.HP;
    }
}

