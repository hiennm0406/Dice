using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLosePanel : MonoBehaviour
{
    /* thiết kế màn win lose như sau:
     * 
     * sau khi user thắng sẽ show màn win. Bên dưới có box show những vật phẩm nhận được trong trận, 
     * Ví dụ normal campaign sẽ nhận được vàng, totem trắng/xanh, mảnh vỡ totem (dùng để nâng cấp totem) 
     * và locked cheast, user sẽ xem video để mở toàn bộ các locked cheast.
     * Sau khi về Home, có anim tăng tiền và có chấm đỏ ở Inventory, những món đồ có thể nâng cấp đc sẽ có chấm đỏ
     * 
     * 
     * Nếu user thua. Màn hình hiện màn lose nhưng vẫn giữ lại hình ảnh God. Lần đầu tiên God sẽ diễn anim suy yếu.
     * Nút xem video để hồi sinh hiện ra. Bên dưới sẽ vẫn có box show vật phẩm có thể nhận được
     * kèm thông báo, you may lose anything
     * sau 3s thì hiện nút "từ bỏ" ở dưới
     * 
     * 
     * Nếu user ấn hồi sinh, màn hình lose biến mất, God diễn anim vùng dậy, đẩy tất cả địch về cuối màn hình, 
     * God sẽ tung xí ngầu ngay sau đấy
     * 
     * Nếu user từ bỏ. God gục ngã, các item phần thưởng sẽ diễn anim đốt cháy, 
     * những vật phẩm không phải god có tỷ lệ 70% bị đốt (mất)
     * gold thì có anim đốt nhưng sau đấy còn lại 30% giá trị.
     * Màn hình lose bị nứt vỡ và tự trở về Home sau 3s.
     * Khi về Home thì có anim tiền tăng lên.
     * 


    */

    [Header("REWARD BOX")]
    public Transform RewardView;
    public ItemBoxUI prefabRewardBox;
    public ScrollRect scrollRect;
    public Button WinBtn;
    public Button NextBtn;

    [Header("LOSE")]
    public Button RetrynBtn;
    public Button RebornBtn;
    public Button SurrenderBtn;
    public GameObject TextLoseDoNothing;
    private bool retryed = false;
    public Text TextCountdown;
    public GameObject ImgCountdown;


    private void Start()
    {
        RebornBtn.onClick.AddListener(Retry);
        UIManager.Instance.winLosePanel = this;
        ClosePopup();
    }

    [Button]
    public void ShowWin()
    {
        StartCoroutine(ShowReward());
        ClaimReward();
        WinBtn.gameObject.SetActive(true);
    }

    [Button]
    public IEnumerator ShowLose()
    {
        BattleManager.Instance.Stage = GAMESTAGE.BREAKPHASE;
        gameObject.SetActive(true);
        StartCoroutine(ShowReward());

        if (GameSave.RETRY)
        {
            GameSave.RETRY = false;
            RebornBtn.gameObject.SetActive(true);
            RetrynBtn.gameObject.SetActive(false);
            TextLoseDoNothing.SetActive(true);
            TextCountdown.gameObject.SetActive(true);
            ImgCountdown.SetActive(true);
            float t = 5;
            while (t >= 0)
            {
                t -= Time.deltaTime;
                if (retryed)
                {
                    yield break;
                }
                TextCountdown.text = Mathf.CeilToInt(t).ToString();
                TextCountdown.transform.localScale = Vector3.Lerp(Vector3.one * 1.5f, Vector3.one, Mathf.CeilToInt(t) - t);
                yield return null;
            }
        }
        GameSave.RETRY = true;
        TextCountdown.gameObject.SetActive(false);
        ImgCountdown.SetActive(false);
        TextLoseDoNothing.SetActive(false);
        RetrynBtn.gameObject.SetActive(true);
        RebornBtn.gameObject.SetActive(false);
        SurrenderBtn.gameObject.SetActive(true);
    }

    [Button]
    public void Retry()
    {
        ClosePopup();
        retryed = true;
        BattleManager.Instance.Retry();
    }

    [Button]
    public void ClosePopup()
    {
        WinBtn.gameObject.SetActive(false);
        RebornBtn.gameObject.SetActive(false);
        SurrenderBtn.gameObject.SetActive(false);
        NextBtn.gameObject.SetActive(false);
        RetrynBtn.gameObject.SetActive(true);
        gameObject.SetActive(false);
        TextLoseDoNothing.SetActive(false);
        TextCountdown.gameObject.SetActive(false);
        ImgCountdown.SetActive(false);
    }

    public void ClaimReward()
    {
        foreach (var item in BattleManager.Instance.rewards)
        {
            bool hav = false;
            foreach (var iteamHave in PlayerData.Instance.ItemList)
            {
                if (iteamHave.ItemId == item.ItemId)
                {
                    hav = true;
                    iteamHave.Number += item.Number;
                    break;
                }
            }
            if (!hav)
            {
                PlayerData.Instance.ItemList.Add(new Item(item.ItemId, item.Number));
            }
        }
    }


    public IEnumerator ShowReward()
    {
        foreach (Transform child in RewardView)
        {
            Destroy(child.gameObject);
        }
        scrollRect.verticalNormalizedPosition = 0;



        foreach (var item in BattleManager.Instance.rewards)
        {
            if (item.ItemId == 0)// soul always first
            {
                ItemBoxUI itemBoxUI = Instantiate(prefabRewardBox, RewardView);
                itemBoxUI.InitItem(item.ItemId, item.Number);
                break;
            }
        }
        yield return Helper.GetWait(0.2f);
        foreach (var item in BattleManager.Instance.rewards)
        {
            if (item.ItemId != 0)// soul always first
            {
                ItemBoxUI itemBoxUI = Instantiate(prefabRewardBox, RewardView);
                itemBoxUI.InitItem(item.ItemId, item.Number);
                yield return Helper.GetWait(0.2f);
            }
        }
    }
}
