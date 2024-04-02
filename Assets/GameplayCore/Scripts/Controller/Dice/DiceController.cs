using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    public int dmg;
    public Element element;
    public List<DmgTag> tags;
    public int DiceId;
    private DiceOnBoardController _diceOnBoard;
    private DiceInfo diceInfo;
    private int[] number;
    public List<DiceImbued> imbued = new List<DiceImbued>();

    protected float dmgImprove = 1;

    private void Start()
    {
        number = new int[6];
        for (int i = 0; i < 6; i++)
        {
            number[i] = i + 1;
        }
        _diceOnBoard = GetComponent<DiceOnBoardController>();
        diceInfo = DiceData.instance.GetDice(DiceId);
        // set up dice
        dmg = diceInfo.baseDmg;
        element = diceInfo.element;
        tags = diceInfo.tags;
    }
    public virtual IEnumerator TriggerDice(int _num)
    {
        // get all tile 
        yield return null;

        DiceInfo _dice = DiceData.instance.GetDice(DiceId);
        int pos = _diceOnBoard.pos;

        List<Tile> tiles = new List<Tile>() { BattleManager.Instance.ListTile[pos] };

        GetTile(tiles, pos, _dice.diceDirection);

        foreach (var item in tiles)
        {
            if (item.unitController != null)
            {
                Debug.Log(dmg + " * " + number[_diceOnBoard.number] + " * " + BattleManager.Instance.godManager.stat.Power);

                DealDmg(item.unitController);
                InflictStatus(item.unitController);
            }
        }
        BattleManager.Instance.done--;
    }


    public virtual void DealDmg(UnitController unit)
    {
        unit.InitDamageWillTake(dmg * number[_diceOnBoard.number] * BattleManager.Instance.godManager.stat.Power, element, tags);
    }

    public virtual void InflictStatus(UnitController unit)
    {

    }

    public void ChangePos()
    {
        DiceInfo _dice = DiceData.instance.GetDice(DiceId);
        int pos = _diceOnBoard.pos;

        List<Tile> tiles = new List<Tile>() { BattleManager.Instance.ListTile[pos] };
        GetTile(tiles, pos, _dice.diceDirection);

        foreach (var item in tiles)
        {
            item.BeingDmg++;
            item.SetDmg();
        }
    }

    public void GetTile(List<Tile> _tiles, int pos, DiceDirection diceDirection)
    {
        foreach (var item in diceDirection.diceDirection)
        {
            int x = -1;
            switch (item.direction)
            {
                case Direction.TOP:
                    x = Helper.GetTop(pos);
                    break;
                case Direction.DOWN:
                    x = Helper.GetDown(pos);
                    break;
                case Direction.RIGHT:
                    x = Helper.GetRight(pos);
                    break;
                case Direction.LEFT:
                    x = Helper.GetLeft(pos);
                    break;
                case Direction.TOPRIGHT:
                    x = Helper.GetTopRight(pos);
                    break;
                case Direction.TOPLEFT:
                    x = Helper.GetTopLeft(pos);
                    break;
                case Direction.DOWNLEFT:
                    x = Helper.GetDownLeft(pos);
                    break;
                case Direction.DOWNRIGHT:
                    x = Helper.GetDownRight(pos);
                    break;
            }

            if (x == -1)
            {
                continue;
            }
            try
            {
                if (!_tiles.Contains(BattleManager.Instance.ListTile[x]))
                {
                    _tiles.Add(BattleManager.Instance.ListTile[x]);
                }
                if (diceDirection.isLoop)
                {
                    GetTile(_tiles, x, diceDirection);
                    return;
                }
                GetTile(_tiles, x, item);
            }
            catch
            {
                Debug.LogError(x);
            }
        }
    }



    #region IMBUE

    public void AddImbue(DiceImbued imbue)
    {
        imbued.Add(imbue);
    }

    public virtual void SetupImbue()
    {
        foreach (var item in imbued)
        {
            switch (item)
            {
                case DiceImbued.INCREASEDMG_I:
                    break;
                case DiceImbued.INCREASEDMG_II:
                    break;
                case DiceImbued.INCREASEDMG_III:
                    break;
            }
        }
    }

    #endregion


}
