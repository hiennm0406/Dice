using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home_BattleUI : MonoBehaviour
{
    [SerializeField] private Button BtnPlay;
    public List<ButtonAction> ListOptionBtn = new List<ButtonAction>();

    // Start is called before the first frame update
    public List<Vector3> listPos;
    public int now;
    private void Start()
    {
        BtnPlay.onClick.AddListener(OnButtonPlay);

        for (int i = 0; i < ListOptionBtn.Count; i++)
        {
            int x = i;
            ListOptionBtn[i].Callback1 = () => Switch(x, ListOptionBtn[x].Callback2);
        }

        listPos.Add(ListOptionBtn[0].transform.position);
        listPos.Add(ListOptionBtn[1].transform.position);
        listPos.Add(ListOptionBtn[2].transform.position);
        listPos.Add(ListOptionBtn[ListOptionBtn.Count - 2].transform.position);
        listPos.Add(ListOptionBtn[ListOptionBtn.Count - 1].transform.position);
    }

    public void OnButtonPlay()
    {
        GameManager.Instance.LoadScene("GamePlay");
    }

    public void Switch(int go, Action callback)
    {
        bool right;
        if (go > now)
        {
            if (now == 0 && go == ListOptionBtn.Count - 1)
            {
                right = false;
            }
            else
            {
                right = true;
            }
        }
        else if (go < now)
        {
            if (now == ListOptionBtn.Count - 1 && go == 0)
            {
                right = true;
            }
            else
            {
                right = false;
            }
        }
        else
        {
            callback?.Invoke();
            return;
        }
        for (int i = 0; i < ListOptionBtn.Count; i++)
        {
            if (i == (go - 2) || i == (go - 2 + ListOptionBtn.Count))
            {
                ListOptionBtn[i].transform.DOMove(listPos[3], 0.5f);
                if (right)
                {
                    ListOptionBtn[i].transform.DOScale(Vector3.one * 0.3f, 0.5f);
                }
                else
                {
                    ListOptionBtn[i].transform.localScale = Vector3.zero;
                }
            }
            else if (i == (go - 1) || i == (go - 1 + ListOptionBtn.Count))
            {
                ListOptionBtn[i].transform.DOMove(listPos[4], 0.5f);
                ListOptionBtn[i].transform.DOScale(Vector3.one * 0.6f, 0.5f);
            }
            else if (i == (go))
            {
                ListOptionBtn[i].transform.DOMove(listPos[0], 0.5f);
                ListOptionBtn[i].transform.DOScale(Vector3.one, 0.5f);
            }
            else if (i == (go + 1) || i == (go + 1 - ListOptionBtn.Count))
            {
                ListOptionBtn[i].transform.DOMove(listPos[1], 0.5f);
                ListOptionBtn[i].transform.DOScale(Vector3.one * 0.6f, 0.5f);
            }
            else if (i == (go + 2) || i == (go + 2 - ListOptionBtn.Count))
            {
                ListOptionBtn[i].transform.DOMove(listPos[2], 0.5f);
                if (!right)
                {
                    ListOptionBtn[i].transform.DOScale(Vector3.one * 0.3f, 0.5f);
                }
                else
                {
                    ListOptionBtn[i].transform.localScale = Vector3.zero;
                }
            }
            else
            {
                ListOptionBtn[i].transform.localScale = Vector3.zero;
            }
        }
        now = go;
    }
}
