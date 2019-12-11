using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        SaveDataManager saveData = GameObject.Find("SaveDataManager").GetComponent<SaveDataManager>();
        itemBuffer.items[0].itemUpgradeIndex = saveData.powerUpgradeIndex;
        itemBuffer.items[1].itemUpgradeIndex = saveData.followersUpgradeIndex;

        int slotCnt = slotRoot.childCount;

        for (int i = 0; i < slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<StoreSlot>();

            if (i < itemBuffer.items.Count)
            {
                ItemPrice findItemIndex = itemPrices.Find(x => (x.itemIndex == i) && (x.upgradeIndex == itemBuffer.items[i].itemUpgradeIndex));
                if (findItemIndex.itemPrice != 0)
                    itemBuffer.items[i].itemPrice = findItemIndex.itemPrice;
                else
                {
                    itemBuffer.items[i].itemPrice = 0;
                    slot.offBuyBtn();
                }
                    
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
            {
                itemBuffer.items[itemIndex].itemPrice = 0;
                slot.offBuyBtn();
            }

            slot.SetItem(itemBuffer.items[itemIndex]);

            switch (itemIndex)
            {
                case 0:
                    saveData.maxPower++;
                    saveData.powerUpgradeIndex++;
                    break;
                case 1:
                    saveData.AddFollowers(itemBuffer.items[itemIndex].itemUpgradeIndex);
                    saveData.followersUpgradeIndex++;
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