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

    public virtual IEnumerator TriggerDice(int number)
    {
        // get all tile 
        Debug.Log("TRIGGER");
        yield return null;

        DiceInfo _dice = DiceData.instance.GetDice(DiceId);
        int pos = GetComponent<DiceOnBoardController>().pos;

        List<Tile> tiles = new List<Tile>() { BattleManager.Instance.ListTile[pos] };

        GetTile(tiles, pos, _dice.diceDirection);

        foreach (var item in tiles)
        {
            if (item.unitController != null)
            {
                item.unitController.TakeDamage(dmg, element, tags);
            }
        }
        BattleManager.Instance.done--;
    }

    public void ChangePos()
    {
        DiceInfo _dice = DiceData.instance.GetDice(DiceId);
        int pos = GetComponent<DiceOnBoardController>().pos;

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
    }

}
