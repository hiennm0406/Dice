using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home_BattleUI : MonoBehaviour
{
    [SerializeField] private Button BtnPlay;

    // Start is called before the first frame update
    private void Start()
    {
        BtnPlay.onClick.AddListener(OnButtonPlay);
    }

    public void OnButtonPlay()
    {
        GameManager.Instance.LoadScene("GamePlay");
    }
}
