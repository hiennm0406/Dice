using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DiceData", menuName = "Data/DiceData", order = 1)]
public class DiceData : SingletonScriptableObject<DiceData>
{
    public List<DiceInfo> listDice = new List<DiceInfo>();
    public DiceInfo GetDice(int id)
    {
        foreach (var item in listDice)
        {
            if (item.DiceId == id)
            {
                return item;
            }
        }
        return null;
    }
}

[System.Serializable]
public class DiceInfo
{
    public int DiceId;
    public string DiceName;
    public Sprite[] SpriteList;
}