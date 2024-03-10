using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    public virtual void TriggerDice(int number)
    {

    }

    public void DealDamage(UnitController target, int dmg, Element element)
    {
        target.HPNow -= dmg;
        if (target.HPNow <= 0)
        {
            target.Die();
        }
    }
}
