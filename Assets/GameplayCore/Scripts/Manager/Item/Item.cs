using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int ItemId;
    public int Number;

    public Item(int itemId, int number)
    {
        ItemId = itemId;
        Number = number;
    }
}
