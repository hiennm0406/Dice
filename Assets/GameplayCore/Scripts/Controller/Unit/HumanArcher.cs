using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanArcher : UnitController
{
    private int attack = 0;
    private bool _attack = false;
    protected void Start()
    {
        attack = 0;
    }

    protected override IEnumerator MoveToPos()
    {
        Anim.SetInteger("Move", 1);
        float t = 0;
        Vector3 start = transform.position;
        Vector3 end = BattleManager.Instance.ListTile[pos].transform.position;
        while (t < 1)
        {
            t += Time.deltaTime * 2;
            transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }
        Anim.SetInteger("Move", 0);
        transform.position = end;

        // check can attack
        attack++;
        if (attack == 2)
        {
            attack = 0;
            yield return StartCoroutine(Attack());
        }

        isMoving = false;
    }


    public override IEnumerator Attack()
    {
        if (Helper.GetCol(pos) <= 0) // if near, archer will shot immidiate
        {
            Debug.Log("UNIT ==> ATTACK GOD " + pos + " " + (Helper.GetCol(pos) + 1) + " " + stat.AtkRange);
            // attack
            Anim.SetTrigger("Attack");
            yield return Helper.GetWait(0.5f);

            BattleManager.Instance.godManager.TakeDamage(ref stat.Power, Element.ALL, null);
            yield return Helper.GetWait(1f);
        }
        else
        {
            Anim.SetTrigger("Special_1");
            _attack = true;
        }
    }


    public override IEnumerator EndTurnAction()
    {
        if (_attack)
        {
            _attack = false;
            Anim.SetTrigger("Special_2");

            yield return Helper.GetWait(0.5f);

            BattleManager.Instance.godManager.TakeDamage(ref stat.Power, Element.ALL, null);
            yield return Helper.GetWait(1f);
        }
    }
}
