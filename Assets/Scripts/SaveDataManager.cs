using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager instance;

    //Set Coin
    public int totalCoin;

    //Set Store Item Effect
    public int maxPower;
    public int[] followers;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void AddFollowers(int Index)
    {
        Array.Resize<int>(ref followers, Index);
    }
}