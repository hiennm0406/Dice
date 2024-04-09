using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : MonoBehaviour
{
    [SerializeField] Button BtnPlay;

    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.homeUI = this;
        BtnPlay.onClick.AddListener(OnButtonPlay);
    }

    public void OnButtonPlay()
    {
        GameManager.Instance.LoadScene("GamePlay");
    }
}
