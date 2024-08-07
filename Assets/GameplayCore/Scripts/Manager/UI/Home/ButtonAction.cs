using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    Button thisButton;
    public Action Callback1;
    public Action Callback2;
    private void Awake()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(ClickBtn);
    }

    public void ClickBtn()
    {
        Callback1.Invoke();
    }
}
