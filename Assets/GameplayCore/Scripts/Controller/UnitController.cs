using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public int UnitId;
    public Vector2Int pos;
    public Stat UnitStat;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("UnitController - SpriteRenderer not found " + gameObject.name);
        }
    }

    public void MoveUnit(Vector2Int _pos, bool move = true)
    {
        pos = _pos;
        spriteRenderer.sortingOrder = pos.x;
        if (move)
        {
            StartCoroutine(MoveToPos());
        }
        else
        {
            transform.position = BattleManager.Instance.ListTile[pos].transform.position;
        }
    }

    IEnumerator MoveToPos()
    {
        float t = 0;
        Vector3 start = transform.position;
        Vector3 end = BattleManager.Instance.ListTile[pos].transform.position;
        while (t < 1)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }
        transform.position = end;
    }
}
