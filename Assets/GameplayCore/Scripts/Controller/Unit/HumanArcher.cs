using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanArcher : UnitController
{
    private int move = 0;
    private bool _attack = false;

    [SerializeField] private GameObject effectToSpawn;
    [SerializeField] private Transform Shooter;
    protected void Start()
    {
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

        move++;
        base.MoveUnit();
    }

    protected override IEnumerator MoveToPos(bool isMove = true)
    {
        Debug.Log(isMove);

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
                move = 0;
                yield return StartCoroutine(AttackSpecial());
            }
        }
        isMoving = false;
    }


    public override IEnumerator Attack()
    {
        yield return Helper.GetWait(0.05f * Random.Range(0, 5));
        Debug.Log("UNIT ==> ATTACK GOD " + pos + " " + (Helper.GetCol(pos) + 1) + " " + stat.AtkRange);
        // attack
        Anim.SetTrigger("Attack");
        yield return Helper.GetWait(0.5f);

        BattleManager.Instance.godManager.TakeDamage(ref stat.Power, Element.ALL, null);
        yield return Helper.GetWait(1f);
    }

    public IEnumerator AttackSpecial()
    {
        yield return Helper.GetWait(0.05f * Random.Range(0, 5));
        Anim.SetTrigger("Special_1");
        yield return Helper.GetWait(0.5f);
    }


    public override IEnumerator EndTurnAction()
    {
        if (_attack)
        {
            yield return Helper.GetWait(0.05f * Random.Range(0, 5));
            _attack = false;
            Anim.SetTrigger("Special_2");
            GameObject vfx = Instantiate(effectToSpawn, Shooter.position, Quaternion.identity);
            ProjectileMoveObj _projectile = vfx.GetComponent<ProjectileMoveObj>();
            _projectile.InitProjectile(new Vector3(BattleManager.Instance.godManager.transform.position.x + 0.85f, Shooter.position.y, 0));
            yield return Helper.GetWait(.5f);

            BattleManager.Instance.godManager.TakeDamage(ref stat.Power, Element.ALL, null);
            yield return Helper.GetWait(1f);
        }
    }

    [Button]
    public void Test()
    {
        GameObject vfx = Instantiate(effectToSpawn, Shooter.position, Quaternion.identity);
        ProjectileMoveObj _projectile = vfx.GetComponent<ProjectileMoveObj>();
        _projectile.InitProjectile(new Vector3(BattleManager.Instance.godManager.transform.position.x + 0.85f, Shooter.position.y, 0));
    }

    public override void Hit()
    {
        if (!_attack)
        {
            Anim.SetTrigger("Hit");
        }
    }
}
