using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImbuedData", menuName = "Data/ImbuedData", order = 0)]
public class ImbuedData : SingletonScriptableObject<ImbuedData>
{
    public List<ImbueInfo> imbueInfos = new List<ImbueInfo>();

    public ImbueInfo GetImbue(DiceImbued _imbue)
    {
        foreach (var item in imbueInfos)
        {
            if (item.imbue == _imbue)
            {
                return item;
            }
        }
        return null;
    }
}

[System.Serializable]
public class ImbueInfo
{
    public DiceImbued imbue;
    public string imbueName;
    public Sprite sprite;
    public string Description;
}