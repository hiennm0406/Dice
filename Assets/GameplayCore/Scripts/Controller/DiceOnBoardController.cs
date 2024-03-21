using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceOnBoardController : MonoBehaviour
{
    public Vector2Int pos;
    public BoxCollider2D boxCollider2D;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
