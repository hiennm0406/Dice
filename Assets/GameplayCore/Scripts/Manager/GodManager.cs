using Sirenix.OdinInspector;
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
    private int levelUp = 0;

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
        HPNow = stat.HP;
    }

    public bool GainExp(int exp)
    {
        expNow += exp;
        int need = ConfigData.instance.ExpLevelUp[Mathf.Min(BattleManager.Instance.godManager.Level, ConfigData.instance.ExpLevelUp.Count - 1)] + 50 * Mathf.Max(0, Level + 1 - ConfigData.instance.ExpLevelUp.Count);

        while (expNow >= need)
        {
            Debug.Log("LEVEL UP " + UIManager.Instance.battleUI.count);

            Level++;
            levelUp++;
            expNow -= need;
            need = ConfigData.instance.ExpLevelUp[Mathf.Min(BattleManager.Instance.godManager.Level, ConfigData.instance.ExpLevelUp.Count - 1)] + 50 * Mathf.Max(0, Level + 1 - ConfigData.instance.ExpLevelUp.Count);
        }

        if (levelUp > 0)
        {
            levelUp--;
            return true;
        }

        return false;
    }

    [Button]
    public override void TakeDamage(ref int _dmg, Element element, List<DmgTag> tags)
    {
        // check all tag logic here

        // immune ? Boss have no immune

        // piearcing? unit can't piercing

        // resis ?
        float resis = 0;
        if (Resis.ContainsKey(element) && element != Element.ALL)
        {
            resis += Resis[element];
        }
        if (Resis.ContainsKey(Element.ALL))
        {
            resis += Resis[Element.ALL];
        }
        float dmg = _dmg * (1f - resis);
        int _finaldmg = Mathf.CeilToInt(dmg);
        HPNow -= _finaldmg;
        Debug.Log("TAKE DMG FINAL ==> " + _finaldmg);

        if (HPNow <= 0)
        {
            // YOU DIE

            UIManager.Instance.winLosePanel.ShowLose();
        }
    }
}

