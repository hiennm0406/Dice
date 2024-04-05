using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceBox : MonoBehaviour
{
    public DiceImbued imbue;
    public DiceController dice;

    [SerializeField] private Image imbueImg;
    [SerializeField] private Text imbueName;
    [SerializeField] private Text imbueDescription;

    [SerializeField] private Button Select;

    private void Start()
    {
        Select.onClick.AddListener(SelectImbue);
    }

    public void InitDiceImbue(DiceImbued _imbue, DiceController _dice)
    {
        imbue = _imbue;
        dice = _dice;

        ImbueInfo _info = dice != null ? ImbuedData.instance.GetImbue(imbue) : ImbuedData.instance.GetImbue(DiceImbued.NULL);

        imbueImg.sprite = _info.sprite;
        imbueName.text = _info.imbueName;
        imbueDescription.text = _info.Description;
    }

    public void SelectImbue()
    {
        if (dice != null)
        {
            dice.AddImbue(imbue);
        }

        UIManager.Instance.battleUI.OnImbueDone();
    }
}
