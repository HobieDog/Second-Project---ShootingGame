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
<<<<<<< HEAD
    public int[] followers;
=======
>>>>>>> 3effc48cd356ac323318c85594b6297a51b292c5

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
<<<<<<< HEAD

    public void AddFollowers(int Index)
    {
        Array.Resize<int>(ref followers, Index);
    }
}
=======
}
>>>>>>> 3effc48cd356ac323318c85594b6297a51b292c5
