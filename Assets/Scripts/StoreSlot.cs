using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreSlot : MonoBehaviour
{
    [HideInInspector]

    public ItemList itemList;
    public Image img;
    public Text itemName;
    public Text itemPrice;
    public Button buyBtn;

    public void onBuyBtn()
    {
        buyBtn.interactable = true;
    }

    public void offBuyBtn()
    {
        buyBtn.interactable = false;
    }

    public void SetItem(ItemList item)
    {
        this.itemList = item;

        if (item == null)
        {
            img.enabled = false;
            gameObject.name = "Empty";
        }
        else
        {
            img.enabled = true;
            gameObject.name = itemList.itemName;
            img.sprite = itemList.sprite;
            itemName.text = itemList.itemName;
            itemPrice.text = string.Format("{0:n0}", itemList.itemPrice);
        }
    }
}
