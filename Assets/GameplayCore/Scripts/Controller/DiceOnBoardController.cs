using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceOnBoardController : MonoBehaviour
{
    public int pos;
    public BoxCollider2D boxCollider2D;
    public SpriteRenderer spriteRenderer;
    public int number;
    public DiceController dice;
    public bool OnBoard;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dice = GetComponent<DiceController>();
        Messenger.AddListener(GameConstant.Event.SET_COLOR, ChangePos);
    }

    public void ChangePos()
    {
        if (OnBoard)
        {
            dice.ChangePos();
        }
        spriteRenderer.sortingOrder = Helper.GetRow(pos);
    }


    private void OnDestroy()
    {
        Messenger.RemoveListener(GameConstant.Event.SET_COLOR, ChangePos);
    }
}
