using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_SkyDice : DiceController
{

    public override void InflictStatus(UnitController unit)
    {
        base.InflictStatus(unit);
        //if (!imbued.Contains(DiceImbued.SUN_BURN_I) && !imbued.Contains(DiceImbued.SUN_BURN_II))
        //{
        //    // inflict sunburn
        //    unit.InitStatusWillTake(StatusManager.instance.GetStatus(StatusID.SUNBURN), 2);
        //}
    }

}
