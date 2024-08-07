using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DiceData", menuName = "Data/DiceData", order = 1)]
public class DiceData : SingletonScriptableObject<DiceData>
{
    public List<Dice> listDice = new List<Dice>();
    public Dice GetDice(int id)
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

[System.Serializable]
public class Dice
{
    [HorizontalGroup("DiceID", 0.2f, LabelWidth = 100)]
    public int DiceId;
    [HorizontalGroup("DiceID", 0.4f, LabelWidth = 100)]
    public string DiceName;
    [HorizontalGroup("DiceID", 0.4f, LabelWidth = 100)]
    public string ClassName;
    public DiceDirection diceDirection;
    public Sprite[] SpriteList;

    [HorizontalGroup("DiceData", 0.4f, LabelWidth = 100)]
    public Element element;

    [HorizontalGroup("DiceEffect", 0.45f, LabelWidth = 100)]
    public GameObject MainEffect;

    [HorizontalGroup("DiceEffect", 0.45f, LabelWidth = 100)]
    public GameObject SubEffect;

    public List<DmgTag> tags;
    public List<DiceImbueMax> diceImbueMax;
    public List<DiceImbueData> diceImbues = new List<DiceImbueData>();

    private void OnValidate()
    {
        foreach (var item in diceImbues)
        {
            item.dice = this;
        }
    }
}
[System.Serializable]
public class DiceImbueMax
{
    [HorizontalGroup("Rate", 200)]
    [LabelWidth(80)]
    public string Name;
    [HorizontalGroup("Rate", 400)]
    [LabelWidth(80)]
    public string Des;
    [HorizontalGroup("Rate", 80)]
    [LabelWidth(50)]
    public int max;
}

[System.Serializable]
public class DiceImbueData
{
    public Dice dice;
    public string Description;
    [SerializeReference]
    public List<DiceImbue> diceImbues = new List<DiceImbue>();

    private void OnValidate()
    {
        Description = "";

        foreach (var item in diceImbues)
        {
            item.dice = dice;
            Description += item.GetDes() + " ";
        }
        Description += ".";
    }
}

[System.Serializable]
public class DiceImbue
{
    public Dice dice;
    public virtual string GetDes()
    {
        return "";
    }
}

[System.Serializable]
public class DiceImbueString : DiceImbue
{
    public string _text;
    public override string GetDes()
    {
        return "";
    }
}


[System.Serializable]
public class DiceImbueValue : DiceImbue
{
    public int num;
    public string _text;
    public override string GetDes()
    {
        if (num == -1)
        {
            return "Dice Roll";
        }
        else
        {
            return dice.diceImbueMax[num].Des;
        }
    }
}



