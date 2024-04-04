using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{

    [SerializeField] private Button DiceTrigger;
    [SerializeField] private Image ExpBar;


    private void Start()
    {
        UIManager.Instance.battleUI = this;
        DiceTrigger.onClick.AddListener(ClickDiceTriggerButton);
        DiceTrigger.gameObject.SetActive(false);
        Messenger.AddListener(GameConstant.Event.EXP_UP, ExpUp);
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

    public void ExpUp()
    {
        float need = ConfigData.instance.ExpLevelUp[Mathf.Min(BattleManager.Instance.godManager.Level, ConfigData.instance.ExpLevelUp.Count - 1)] + 50 * Mathf.Max(0, BattleManager.Instance.godManager.Level + 1 - ConfigData.instance.ExpLevelUp.Count);
        float x = BattleManager.Instance.godManager.expNow / need;
        ExpBar.fillAmount = x;
    }


    private void OnDestroy()
    {
        Messenger.RemoveListener(GameConstant.Event.EXP_UP, ExpUp);
    }
}
