using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Data/UnitData", order = 1)]
public class UnitData : SingletonScriptableObject<UnitData>
{
    [TableList]
    public List<UnitEnemy> ListEnemy = new List<UnitEnemy>();

    public UnitEnemy GetUnitEnemy(int id)
    {
        foreach (var item in ListEnemy)
        {
            if (item.UnitId == id)
            {
                return item;
            }
        }
        return null;
    }
}
[System.Serializable]
public class UnitEnemy
{
    [VerticalGroup("Info")]
    public int UnitId;
    [VerticalGroup("Info")]
    public string UnitName;
    [PreviewField(50)]
    public Sprite ImageUnit;
    [PreviewField(50)]
    public GameObject Prefab;

    public List<Skill> Skills;
    public Stat BaseStat;
}