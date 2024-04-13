using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanArcher : UnitController
{
    private int move = 0;
    private int attack = 0;
    private bool _attack = false;
    protected void Start()
    {
        attack = 0;
        move = 0;
    }

    public override void MoveUnit()
    {
        isMoving = true;
        if (Helper.GetCol(pos) == 0)
        {
            //attack
            StartCoroutine(MoveToPos(false));
            return;
        }

        if (!_attack)
        {
            move++;
            canBack = false;
            base.MoveUnit();

        }
        else
        {
            attack++;
            canBack = true;
            isMoving = false;
        }
    }

    protected override IEnumerator MoveToPos(bool isMove = true)
    {
        if (isMove)
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
        }
        // check can attack
        if (Helper.GetCol(pos) == 0)
        {
            yield return StartCoroutine(Attack());
        }
        else
        {
            if (move == 2)
            {
                _attack = true;
                attack = 0;
                move = 0;
                yield return StartCoroutine(AttackSpecial());
            }
        }
        isMoving = false;
    }


    public override IEnumerator Attack()
    {
        Debug.Log("UNIT ==> ATTACK GOD " + pos + " " + (Helper.GetCol(pos) + 1) + " " + stat.AtkRange);
        // attack
        Anim.SetTrigger("Attack");
        yield return Helper.GetWait(0.5f);

        BattleManager.Instance.godManager.TakeDamage(ref stat.Power, Element.ALL, null);
        yield return Helper.GetWait(1f);
    }

    public IEnumerator AttackSpecial()
    {
        Anim.SetTrigger("Special_1");
        yield return Helper.GetWait(0.5f);
    }


    public override IEnumerator EndTurnAction()
    {
        if (_attack && attack == 1)
        {
            Debug.Log("archer hit!!");

            canBack = false;
            _attack = false;
            Anim.SetTrigger("Special_2");

            yield return Helper.GetWait(0.5f);

            BattleManager.Instance.godManager.TakeDamage(ref stat.Power, Element.ALL, null);
            yield return Helper.GetWait(1f);
        }
    }

    public override void Hit()
    {
        if (!_attack)
        {
            Anim.SetTrigger("Hit");
        }
    }
}
