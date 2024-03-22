using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_FireDice : DiceController
{
    public override void TriggerDice(int number)
    {
        // deal dmg các ô xung quanh
        DiceOnBoardController _dice = GetComponent<DiceOnBoardController>();

    }
}
