using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : MonoBehaviour
{
    [SerializeField] private Home_BattleUI home_BattleUI;

    // Start is called before the first frame update
    private void Start()
    {
        UIManager.Instance.homeUI = this;
    }

}
