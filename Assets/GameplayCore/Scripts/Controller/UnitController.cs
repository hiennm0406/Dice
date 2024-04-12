using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : UnitBase
{
    public int UnitId;
    public int pos;
    public bool isMoving;
    public int exp;
    public List<TakeDamage> dmg = new List<TakeDamage>();
    public List<TakeStatus> statusWaiting = new List<TakeStatus>();
    #region privateStat
    protected SpriteRenderer spriteRenderer;
    #endregion

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("UnitController - SpriteRenderer not found " + gameObject.name);
        }
    }

    public void StartMoveUnit(int _pos)
    {
        isMoving = true;
        pos = _pos;
        transform.position = BattleManager.Instance.ListTile[pos].transform.position + new Vector3(2, 0, 0);
        spriteRenderer.sortingOrder = Helper.GetRow(pos);
        BattleManager.Instance.ListTile[pos].unitController = this;

        StartCoroutine(MoveToPos());
    }

    public virtual void MoveUnit()
    {
        BattleManager.Instance.ListTile[pos].unitController = null;
        isMoving = true;
        int _y = Helper.GetCol(pos) - stat.Moving;

        if (_y <= 0)
        {
            _y = 0;
        }
        pos = Helper.GetIVector(Helper.GetRow(pos), _y);
        spriteRenderer.sortingOrder = 100 - Helper.GetRow(pos);
        BattleManager.Instance.ListTile[pos].unitController = this;

        StartCoroutine(MoveToPos());
    }

    protected virtual IEnumerator MoveToPos()
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
        yield return StartCoroutine(Attack());
        isMoving = false;
    }

    public virtual IEnumerator Attack()
    {
        if (Helper.GetCol(pos) + 1 <= stat.AtkRange)
        {
            Debug.Log("UNIT ==> ATTACK GOD " + pos + " " + (Helper.GetCol(pos) + 1) + " " + stat.AtkRange);
            // attack
            Anim.SetTrigger("Attack");
            yield return Helper.GetWait(0.5f);

            BattleManager.Instance.godManager.TakeDamage(ref stat.Power, Element.ALL, null);
            yield return Helper.GetWait(1f);
        }
    }


    public void InitDamageWillTake(int _dmg, Element element, List<DmgTag> tags)
    {
        dmg.Add(new TakeDamage(_dmg, element, tags));
    }

    public void InitStatusWillTake(Status _status, int _duration)
    {
        Debug.Log("TAKE STATUS ==> " + _status.statusName);
        statusWaiting.Add(new TakeStatus(_status, _duration));
    }

    public IEnumerator EndTurn()
    {
        int _hp = HPNow;
        foreach (var item in dmg)
        {
            TakeDamage(ref item.dmg, item.element, item.tags);

            // anim chữ bay lên (item.dmg)
        }

        dmg.Clear();
        BattleManager.Instance.done--;

        if (HPNow <= 0)
        {
            statusWaiting.Clear();
            status.Clear();
            Die();
            yield break;
        }
        if (HPNow != _hp)
        {
            Anim.SetTrigger("Hit");
        }

        foreach (var item in statusWaiting)
        {
            status.Add(item);
            item.status.OnTrigger(this);
        }
        statusWaiting.Clear();
        // trigger
        foreach (var item in status)
        {
            item.Duration--;
            if (item.Duration == 0)
            {
                item.status.OnRemove(this);
            }
        }
        yield return StartCoroutine(EndTurnAction());
    }

    public virtual IEnumerator EndTurnAction()
    {
        yield return null;
    }

    public void Die()
    {
        BattleManager.Instance.GainExp(exp);
        BattleManager.Instance.ListTile[pos].unitController = null;
        BattleManager.Instance.listUnit.Remove(this);
        Anim.SetTrigger("Die");
        Destroy(gameObject, 1f);
    }
}

[System.Serializable]
public class TakeDamage
{
    public int dmg;
    public Element element;
    public List<DmgTag> tags;

    public TakeDamage(int dmg, Element element, List<DmgTag> tags)
    {
        this.dmg = dmg;
        this.element = element;
        this.tags = tags;
    }
}

[System.Serializable]
public class TakeStatus
{
    public Status status;
    public int Duration;

    public TakeStatus(Status _status, int _duration)
    {
        this.status = _status;
        this.Duration = _duration;
    }
}