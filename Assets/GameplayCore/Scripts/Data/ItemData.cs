using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Data/ItemData", order = 1)]
public class ItemData : SingletonScriptableObject<ItemData>
{
    public Sprite Chest;
    public Sprite ChestOpen;
    public List<ItemInfo> itemInfos = new List<ItemInfo>();

    public ItemInfo GetItem(int id)
    {
        foreach (var item in itemInfos)
        {
            if (item.ItemId == id)
            {
                return item;
            }
        }
        return null;
    }
}

[System.Serializable]
public class ItemInfo
{
    public int ItemId;
    [HorizontalGroup("Data", LabelWidth = 80)]
    public ItemType type;
    [HorizontalGroup("Data", LabelWidth = 80)]
    public string ItemName;
    [PreviewField]
    [HorizontalGroup("Data", LabelWidth = 80)]
    public Sprite img;
}

public enum ItemType
{
    RESOURCE,
    TOTEM
}
