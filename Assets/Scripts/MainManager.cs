using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    //Main UI Setting
    public Text coinText;
    public SaveDataManager saveData;
    public GameObject storeUI;

    void Awake()
    {
        saveData = GameObject.Find("SaveDataManager").GetComponent<SaveDataManager>();
        storeUI.SetActive(false);
    }


    void Update()
    {
        //Print Total Coin
        coinText.text = string.Format("{0:n0}", saveData.totalCoin);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Loading");
    }

    public void OpenStore()
    {
        storeUI.SetActive(true);
    }

    public void CloseStore()
    {
        storeUI.SetActive(false);
    }
}
