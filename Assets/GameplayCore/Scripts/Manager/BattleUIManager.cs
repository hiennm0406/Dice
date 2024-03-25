using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{

    [SerializeField] Button DiceTrigger;

    private void Start()
    {
        UIManager.Instance.battleUI = this;
        DiceTrigger.onClick.AddListener(ClickDiceTriggerButton);
        DiceTrigger.gameObject.SetActive(false);
    }

    public void ShowButtonTrigger()
    {
        DiceTrigger.gameObject.SetActive(true);
    }

    public void ClickDiceTriggerButton()
    {
        BattleManager.Instance.TriggerDice();
        DiceTrigger.gameObject.SetActive(false);
    }
}
