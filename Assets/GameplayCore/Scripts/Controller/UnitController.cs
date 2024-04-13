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
    public bool canBack = false;
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
        isMoving = true;
        // check pos to go 
        for (int i = stat.Moving; i > 0; i--)
        {
            int _y = Helper.GetCol(pos) - i;

            if (_y <= 0)
            {
                _y = 0;
                StartCoroutine(MoveToPos(false));
                return;
            }
            int _newPos = Helper.GetIVector(Helper.GetRow(pos), _y);
            if (BattleManager.Instance.ListTile[_newPos].unitController != null)
            {
                // đã có unit rồi
                // check xem unit ở ô đó có thể lùi không
                if (BattleManager.Instance.ListTile[_newPos].unitController.canBack)
                {
                    // chỉ lùi 1 ô 
                    if (i > 1) // nếu nhân vật hiện tại di chuyển nhiều hơn 1 ô -> check ô lùi xem có trống không
                    {
                        int movebackPos = Helper.GetIVector(Helper.GetRow(_newPos), Helper.GetCol(_newPos) + 1);
                        if (BattleManager.Instance.ListTile[movebackPos].unitController != null)
                        {
                            // có unit khác nữa ở ô này rồi, k lùi đc đứng im thôi
                            continue;
                        }
                        else
                        {
                            // lùi
                            BattleManager.Instance.ListTile[_newPos].unitController.canBack = false;
                            BattleManager.Instance.ListTile[_newPos].unitController.pos = movebackPos;
                            StartCoroutine(BattleManager.Instance.ListTile[_newPos].unitController.MoveBack());
                            BattleManager.Instance.ListTile[movebackPos].unitController = BattleManager.Instance.ListTile[_newPos].unitController;
                            BattleManager.Instance.ListTile[_newPos].unitController = null;
                        }
                    }
                    else // nếu nhân vật hiện tại di chuyển 1 ô => ô lùi cũng chính là ô nhân vật này đang đứng, swap vị trí 2 người
                    {
                        BattleManager.Instance.ListTile[_newPos].unitController.canBack = false;
                        BattleManager.Instance.ListTile[_newPos].unitController.pos = pos;
                        StartCoroutine(BattleManager.Instance.ListTile[_newPos].unitController.MoveBack());
                        BattleManager.Instance.ListTile[pos].unitController = BattleManager.Instance.ListTile[_newPos].unitController;
                        BattleManager.Instance.ListTile[_newPos].unitController = null;
                    }
                    pos = _newPos;
                    spriteRenderer.sortingOrder = 100 - Helper.GetRow(pos);
                    BattleManager.Instance.ListTile[pos].unitController = this;
                    StartCoroutine(MoveToPos());
                    return;
                }
                else
                {
                    if (i == 1)
                    {
                        StartCoroutine(MoveToPos(false));
                        return;
                    }
                }
            }
            else
            {
                // move to pos 
                BattleManager.Instance.ListTile[pos].unitController = null;
                pos = _newPos;
                spriteRenderer.sortingOrder = 100 - Helper.GetRow(pos);
                BattleManager.Instance.ListTile[pos].unitController = this;
                StartCoroutine(MoveToPos());
                return;
            }
        }
    }

    protected virtual IEnumerator MoveToPos(bool isMove = true)
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
        yield return StartCoroutine(Attack());
        isMoving = false;
    }

    public IEnumerator MoveBack()
    {
        float t = 0;
        Vector3 start = transform.position;
        Vector3 end = BattleManager.Instance.ListTile[pos].transform.position;
        while (t < 1)
        {
            t += Time.deltaTime * 2;
            transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }
        transform.position = end;
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

        if (HPNow <= 0)
        {
            statusWaiting.Clear();
            status.Clear();
            BattleManager.Instance.done--;
            Die();
            yield break;
        }
        if (HPNow != _hp)
        {
            Hit();
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
        yield return Helper.GetWait(0.5f);
        yield return StartCoroutine(EndTurnAction());
        BattleManager.Instance.done--;
    }

    public virtual IEnumerator EndTurnAction()
    {
        yield return null;
    }

    public virtual void Die()
    {
        BattleManager.Instance.GainExp(exp);
        BattleManager.Instance.ListTile[pos].unitController = null;
        BattleManager.Instance.listUnit.Remove(this);
        Anim.SetTrigger("Die");
        Destroy(gameObject, 1f);
    }

    public virtual void Hit()
    {
        Anim.SetTrigger("Hit");
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