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
    public int Pos;
    public int BeingDmg = 0;
    private SpriteRenderer render;
    public void Start()
    {
        render = GetComponent<SpriteRenderer>();
        BattleManager.Instance.ListTile.Add(Helper.GetIVector(Row, Col), this);
        Messenger.AddListener(GameConstant.Event.RESET_COLOR, ResetColor);
    }

    public void ResetColor()
    {
        BeingDmg = 0;
        SetDmg();
    }

    public void SetDmg()
    {
        float _col = 0;
        for (int i = 0; i < BeingDmg; i++)
        {
            _col += 0.2f;
        }

        render.color = Helper.ChangeSaturation(render.color, _col);
    }

    [Button]
    public void Down()
    {
        Row--;
    }

    [Button]
    public void GetPos()
    {
        Pos = Helper.GetIVector(Row, Col);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameConstant.Event.RESET_COLOR, ResetColor);
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
