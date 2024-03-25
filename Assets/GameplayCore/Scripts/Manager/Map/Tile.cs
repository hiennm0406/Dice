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

    [Button]
    public void ResetColor()
    {
        BeingDmg = 0;
        SetDmg();
    }

    public void SetDmg()
    {
        if (BeingDmg == 0)
        {
            if ((Row + Col) % 2 == 0)
            {
                render.color = Helper.ChangeSaturation(render.color, 0, 0.9f);
            }
            else
            {
                render.color = Helper.ChangeSaturation(render.color, 0, 1f);
            }
        }
        else
        {
            float _col = 0;
            for (int i = 0; i < BeingDmg; i++)
            {
                _col += 0.25f;
            }
            render.color = Helper.ChangeSaturation(render.color, _col, 1f);
        }
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
        gameObject.name = "Row" + Row + " - Col" + Col;
    }

#endif
}
