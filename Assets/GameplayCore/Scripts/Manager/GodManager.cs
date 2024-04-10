using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodManager : UnitBase
{
    public int GodId;
    public int Level;
    public int expNow;
    public God godData;
    public Dictionary<Element, float> Improve = new Dictionary<Element, float>();
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
        stat.Power = godData.BaseStat.Power + (Level + 1) * godData.BaseStatIncrease.Power;
        stat.HP = godData.BaseStat.HP + (Level + 1) * godData.BaseStatIncrease.HP;
    }

    public bool GainExp(int exp)
    {
        expNow += exp;
        int need = ConfigData.instance.ExpLevelUp[Mathf.Min(BattleManager.Instance.godManager.Level, ConfigData.instance.ExpLevelUp.Count - 1)] + 50 * Mathf.Max(0, Level + 1 - ConfigData.instance.ExpLevelUp.Count);

        if (expNow >= need)
        {
            Debug.Log("LEVEL UP " + UIManager.Instance.battleUI.count);

            Level++;
            expNow -= need;
            return true;
        }

        return false;
    }
}

