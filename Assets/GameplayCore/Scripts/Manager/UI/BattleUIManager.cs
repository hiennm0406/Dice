using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{

    [SerializeField] private Button DiceTrigger;
    [SerializeField] private Image ExpBar;
    [SerializeField] public List<ChoiceBox> choiceBoxes = new List<ChoiceBox>();
    [SerializeField] private GameObject ChoicePanel;


    public int count = 0;
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


    public void ShowImbueChoice(DiceImbued diceImbued, DiceController dice = null)
    {
        //Debug.Log("imbue => count" + count);
        BattleManager.Instance.UserBusy = true;
        try
        {

            choiceBoxes[count].InitDiceImbue(diceImbued, dice);
        }
        catch
        {
            Debug.LogError(count);

        }
        count++;
    }

    public void ShowMenuImbueChoice()
    {
        //Debug.Log("imbue => show" + count);
        if (count == 3)
        {
            ChoicePanel.SetActive(true);
        }
    }

    public void OnImbueDone()
    {
        //Debug.Log("imbue => done");
        count = 0;
        ChoicePanel.SetActive(false);
        BattleManager.Instance.UserBusy = false;

        BattleManager.Instance.GainExp(0);
    }


    private void OnDestroy()
    {
        Messenger.RemoveListener(GameConstant.Event.EXP_UP, ExpUp);
    }
}
