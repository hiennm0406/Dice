using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tile : MonoBehaviour
{
    public int Row;
    public int Col;
    public bool free;
    public UnitController unitController;
    public void Start()
    {
        BattleManager.Instance.ListTile.Add(Helper.GetVector(Row, Col), this);
    }

    [Button]
    public void Down()
    {
        Row--;
    }
#if UNITY_EDITOR
    [Button]
    public void Get()
    {
        Col = Mathf.RoundToInt(transform.localPosition.x);
        Row = Mathf.RoundToInt(transform.localPosition.y) + 4;
    }

#endif
}
