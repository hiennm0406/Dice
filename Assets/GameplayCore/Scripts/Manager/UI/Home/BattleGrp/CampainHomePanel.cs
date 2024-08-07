using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampainHomePanel : MonoBehaviour
{
    public ButtonAction buttonAction;
    private void Start()
    {
        buttonAction.Callback2 = ShowPanel;
        gameObject.SetActive(false);
    }

    public void ShowPanel()
    {
        Debug.Log("show panel");

    }
}
