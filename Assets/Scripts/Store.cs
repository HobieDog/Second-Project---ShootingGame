using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public ItemBuffer itemBuffer;
    public Transform slotRoot;

    private List<StoreSlot> slots;

    public void Start()
    {
        slots = new List<StoreSlot>();
        int slotCnt = slotRoot.childCount;

        for (int i = 0; i < slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<StoreSlot>();

            if (i < itemBuffer.items.Count)
            {
                slot.SetItem(itemBuffer.items[i]);
            }

            slots.Add(slot);
        }
    }

    public void PriceUp(int itemIndex)
    {
        SaveDataManager saveData = GameObject.Find("SaveDataManager").GetComponent<SaveDataManager>();
        var slot = slotRoot.GetChild(itemIndex).GetComponent<StoreSlot>();

        switch (itemIndex)
        {
            case 0:
                if (saveData.totalCoin >= itemBuffer.items[itemIndex].itemPrice && itemBuffer.items[itemIndex].itemUpgradeIndex <= 5)
                {
                    saveData.totalCoin -= itemBuffer.items[itemIndex].itemPrice;
                    itemBuffer.items[itemIndex].itemPrice += 500;
                    itemBuffer.items[itemIndex].itemUpgradeIndex++;
                    saveData.maxPower++;
                    slot.SetItem(itemBuffer.items[itemIndex]);
                }
                else
                    slot.offBuyBtn();
                break;
            case 1:
                if (saveData.totalCoin >= itemBuffer.items[itemIndex].itemPrice && itemBuffer.items[itemIndex].itemUpgradeIndex <= 1)
                {
                    saveData.totalCoin -= itemBuffer.items[itemIndex].itemPrice;
                    itemBuffer.items[itemIndex].itemPrice += 500;
                    itemBuffer.items[itemIndex].itemUpgradeIndex++;
                    saveData.maxPower++;
                    slot.SetItem(itemBuffer.items[itemIndex]);
                }
                else
                    slot.offBuyBtn();
                break;
            case 2:
                if (saveData.totalCoin >= itemBuffer.items[itemIndex].itemPrice && itemBuffer.items[itemIndex].itemUpgradeIndex <= 1)
                {
                    saveData.totalCoin -= itemBuffer.items[itemIndex].itemPrice;
                    itemBuffer.items[itemIndex].itemPrice += 500;
                    itemBuffer.items[itemIndex].itemUpgradeIndex++;
                    saveData.maxPower++;
                    slot.SetItem(itemBuffer.items[itemIndex]);
                }
                else
                    slot.offBuyBtn();
                break;
        }
    }
}
