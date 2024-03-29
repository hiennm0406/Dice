using Sirenix.OdinInspector;
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
    [HorizontalGroup("DiceID", 0.2f, LabelWidth = 100)]
    public int DiceId;
    [HorizontalGroup("DiceID", 0.5f, LabelWidth = 100)]
    public string DiceName;
    public DiceDirection diceDirection;
    public Sprite[] SpriteList;
    [HideLabel]
    [HorizontalGroup("DiceData", 50)]
    public bool isTemp = false;
    [HorizontalGroup("DiceData", 0.4f, LabelWidth = 100)]
    public int baseDmg = 1;
    [HorizontalGroup("DiceData", 0.4f, LabelWidth = 100)]
    public Element element;
    public List<DmgTag> tags;
}

[System.Serializable]
public class DiceDirection
{
    public Direction direction;
    [OnValueChanged("OnChangeLoop")]
    public bool isLoop;
    public List<DiceDirection> diceDirection = new List<DiceDirection>();

    public void OnChangeLoop()
    {
        if (isLoop)
        {
            diceDirection = null;
        }
        else
        {
            diceDirection = new List<DiceDirection>();
        }
    }
}

public enum Direction
{
    CENTER,
    TOP,
    DOWN,
    RIGHT,
    LEFT,
    TOPRIGHT,
    TOPLEFT,
    DOWNRIGHT,
    DOWNLEFT
}