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
    [HorizontalGroup("DiceID", 0.4f, LabelWidth = 100)]
    public string DiceName;
    [HorizontalGroup("DiceID", 0.4f, LabelWidth = 100)]
    public string ClassName;
    public DiceDirection diceDirection;
    public Sprite[] SpriteList;
    [HideLabel]
    [HorizontalGroup("DiceData", 50)]
    public bool isTemp = false;
    [HorizontalGroup("DiceData", 0.4f, LabelWidth = 100)]
    public int baseDmg = 1;
    [HorizontalGroup("DiceData", 0.4f, LabelWidth = 100)]
    public Element element;

    [HorizontalGroup("DiceEffect", 0.4f, LabelWidth = 100)]
    public GameObject MainEffect;

    [HorizontalGroup("DiceEffect", 0.4f, LabelWidth = 100)]
    public GameObject SubEffect;

    public List<DmgTag> tags;
    public List<DiceImbue> diceImbues = new List<DiceImbue>();
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
public class DiceImbue
{
    public DiceImbued ImbueName; // UNITQUE
    [HorizontalGroup("Required", 0.3f)]
    public List<DiceImbued> required = new List<DiceImbued>(); // need id to unlock
    [HorizontalGroup("Required", 0.3f)]
    public List<int> GodRequired = new List<int>(); // only for this god
    [HorizontalGroup("Required", 0.3f)]
    public List<GodTalen> GodTalenRequired = new List<GodTalen>(); // god need this to unlock
    [HorizontalGroup("Rate", 0.3f)]
    public int Rate;
    [HorizontalGroup("Rate", 0.3f)]
    [MinValue(1)]
    public int maxCount = 1;// if > 1 => can reoccus multi time
}


public enum DiceImbued
{
    NULL,
    INCREASEDMG_I,
    INCREASEDMG_II,
    INCREASEDMG_III,
    SUN_BURN_I,
    SUN_BURN_II,
}