using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public int UnitId;
    public Vector2Int pos;
    public Stat UnitStat;
    public bool isMoving;
    public int HPNow;
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

    public void StartMoveUnit(Vector2Int _pos)
    {
        isMoving = true;
        pos = _pos;
        transform.position = BattleManager.Instance.ListTile[pos].transform.position + new Vector3(2, 0, 0);
        spriteRenderer.sortingOrder = pos.x;
        BattleManager.Instance.ListTile[pos].unitController = this;

        StartCoroutine(MoveToPos());
    }

    public virtual void MoveUnit()
    {
        BattleManager.Instance.ListTile[pos].unitController = null;
        isMoving = true;
        pos.y -= UnitStat.Moving;
        spriteRenderer.sortingOrder = pos.x;
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

    public void Die()
    {
        BattleManager.Instance.ListTile[pos] = null;
        BattleManager.Instance.listUnit.Remove(this);
        Destroy(gameObject);
    }
}
