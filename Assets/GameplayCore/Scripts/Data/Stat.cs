using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Stat
{
    [FoldoutGroup("Stat", expanded: false)]
    public int Power;
    [FoldoutGroup("Stat")]
    public int HP;
    [FoldoutGroup("Stat")]
    public int Moving;
    [FoldoutGroup("Stat")]
    public int AtkRange;
    [FoldoutGroup("Stat")]
    public int Luck;
    [FoldoutGroup("Stat")]
    public int CritRate;
    [FoldoutGroup("Stat")]
    public int CritDmg;

    public void CopyStat(Stat _stat)
    {
        Power = _stat.Power;
        HP = _stat.HP;
        Moving = _stat.Moving;
        AtkRange = _stat.AtkRange;
        Luck = _stat.Luck;
        CritRate = _stat.CritRate;
        CritDmg = _stat.CritDmg;
    }
}
