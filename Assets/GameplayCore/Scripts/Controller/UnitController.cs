using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public int UnitId;
    public int pos;
    public Stat UnitStat;
    public bool isMoving;
    public int HPNow;

    public List<TakeDamage> dmg = new List<TakeDamage>();
    #region privateStat
    private SpriteRenderer spriteRenderer;
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
        int _y = Helper.GetCol(pos) - UnitStat.Moving;

        if (_y <= 0)
        {
            _y = 0;
        }
        pos = Helper.GetIVector(Helper.GetRow(pos), _y);
        Debug.Log(pos + " new col : " + _y);
        spriteRenderer.sortingOrder = Helper.GetRow(pos);
        BattleManager.Instance.ListTile[pos].unitController = this;

        StartCoroutine(MoveToPos());
    }

    private IEnumerator MoveToPos()
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
        isMoving = false;
    }

    public void EndTurn()
    {
        foreach (var item in dmg)
        {
            HPNow -= item.dmg;
        }
    }

    public void Die()
    {
        BattleManager.Instance.ListTile[pos] = null;
        BattleManager.Instance.listUnit.Remove(this);
        Destroy(gameObject);
    }
}


public class TakeDamage
{
    public int dmg;
    public Element element;
}