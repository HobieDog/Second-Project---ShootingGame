using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager instance;

    //Set Coin
    public int totalCoin;

    //Set Store Item Effect
    public int maxPower;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
}
