using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    //Main UI Setting
    public Text coinText;

    //OnClick Check
    public bool isPlaying;

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
        if (isPlaying)
            return;
        isPlaying = true;

        Rigidbody2D startBtn = GameObject.Find("StartBtn").GetComponent<Rigidbody2D>();
        startBtn.AddForce(Vector2.up * 80, ForceMode2D.Impulse);
        Invoke("MoveGameScene", 3);
    }

    public void MoveGameScene()
    {
        isPlaying = false;
        SceneManager.LoadScene("Loading");
    }
}
