using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemBoxUI : MonoBehaviour
{
    private Button button;
    public Text ValueText;
    public int id;
    public int value;
    [SerializeField] private Image imgItem;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickBtn);
    }

    public void InitItem(int _id, int _val)
    {
        id = _id;
        value = _val;
        ValueText.text = _val.ToString();

        ItemInfo _item = ItemData.instance.GetItem(id);
        imgItem.sprite = _item.img;
    }


    public void OnClickBtn()
    {
        Debug.Log("Hello World");
    }
}
