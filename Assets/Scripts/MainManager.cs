using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    //Main UI Setting
    public Text coinText;

    void Awake()
    {

    }


    void Update()
    {
        SaveDataManager saveData = GameObject.Find("SaveDataManager").GetComponent<SaveDataManager>();
        //Print Total Coin
        coinText.text = string.Format("{0:n0}", saveData.totalCoin);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Loading");
    }
}
