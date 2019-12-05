using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager instance;
    public int stageCoin;
    public int totalCoin;

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

    public void SetCoin()
    {
        totalCoin += stageCoin;
        stageCoin = 0;
    }
}
