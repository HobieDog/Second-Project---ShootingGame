using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Store : MonoBehaviour
{
    public ItemBuffer itemBuffer;
    public Transform slotRoot;

    private List<StoreSlot> slots;
    private List<ItemPrice> itemPrices;

    void Awake()
    {
        slots = new List<StoreSlot>();
        itemPrices = new List<ItemPrice>();
        ReadItemPriceFile();
    }

    public void Start()
    {

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

    public void ReadItemPriceFile()
    {
        //Variable Initialization
        itemPrices.Clear();

        //Item Price File Read
        TextAsset textFile = Resources.Load("ItemPrice") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {

            string line = stringReader.ReadLine();

            if (line == null)
                break;

            //Item Price Data construct
            ItemPrice itemPriceData = new ItemPrice();
            itemPriceData.itemIndex = int.Parse(line.Split(',')[0]);
            itemPriceData.upgradeIndex = int.Parse(line.Split(',')[1]);
            itemPriceData.itemPrice = int.Parse(line.Split(',')[2]);
            itemPrices.Add(itemPriceData);
        }

        //Text File Close
        stringReader.Close();
    }

    public void PriceUp(int itemIndex)
    {
        SaveDataManager saveData = GameObject.Find("SaveDataManager").GetComponent<SaveDataManager>();
        var slot = slotRoot.GetChild(itemIndex).GetComponent<StoreSlot>();

        if (saveData.totalCoin >= itemBuffer.items[itemIndex].itemPrice)
        {
            slot.onBuyBtn();
            saveData.totalCoin -= itemBuffer.items[itemIndex].itemPrice;

            itemBuffer.items[itemIndex].itemUpgradeIndex++;

            ItemPrice findItemIndex = itemPrices.Find(x => (x.itemIndex == itemIndex) && (x.upgradeIndex == itemBuffer.items[itemIndex].itemUpgradeIndex));
            if (findItemIndex.itemPrice != 0)
                itemBuffer.items[itemIndex].itemPrice = findItemIndex.itemPrice;
            else
                slot.offBuyBtn();

            slot.SetItem(itemBuffer.items[itemIndex]);

            switch (itemIndex)
            {
                case 0:
                    saveData.maxPower++;
                    break;
                case 1:
                    saveData.AddFollowers(itemBuffer.items[itemIndex].itemUpgradeIndex); //미구현
                    break;
                case 2:
                    saveData.maxPower++; //미구현
                    break;
            }
        }
        else
            slot.offBuyBtn();
    }
}