using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceBox : MonoBehaviour
{
    public DiceImbued imbue;
    public DiceController dice;

    [SerializeField] private Button Select;

    private void Start()
    {
        Select.onClick.AddListener(SelectImbue);
    }

    public void InitDiceImbue(DiceImbued _imbue, DiceController _dice)
    {
        imbue = _imbue;
        dice = _dice;
    }

    public void SelectImbue()
    {
        dice.AddImbue(imbue);
        UIManager.Instance.battleUI.OnImbueDone();
    }
}
