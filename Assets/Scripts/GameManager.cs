using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public string[] enemyObjs;
    public Transform[] spawnPoints;

    //Spawn Delay Setting
    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;

    //GameManager UI Setting
    public Text scoreText;
    public Text boomText;
    public Image[] lifeImg;
    public Image boomImg;
    public GameObject gameOverSet;

    //Object Manager
    public ObjManager objManager;

    //Spawn
    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;


    void Awake()
    {
        spawnList = new List<Spawn>();
        enemyObjs = new string[] { "EnemyS", "EnemyM", "EnemyL", "Boss" };
        ReadSpawnFile();
    }

    void ReadSpawnFile()
    {
        //Variable Initialization
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        //ReSpawn File Read
        TextAsset textFile = Resources.Load("Stage 0") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {

            string line = stringReader.ReadLine();

            if (line == null)
                break;

            //ReSpawn Data construct
            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        //Text File Close
        stringReader.Close();

        //Spawn Delay Time
        nextSpawnDelay = spawnList[0].delay;
    }

    void Update()
    {
        //Spawn Delay
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }

        //UI Score Update
        Player playerLogic = player.GetComponent<Player>();
        //{0:n0} == 999,999,999の,
        scoreText.text = string.Format("{0:n0}", playerLogic.score);

        //UI Boom Update
        boomText.text = playerLogic.boomCount.ToString();
    }

    void SpawnEnemy()
    {
        //Spawn Enemy Type
        int enemyIndex = 1;
        switch (spawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "L":
                enemyIndex = 2;
                break;
            case "B":
                enemyIndex = 3;
                break;
        }

        //Spawn Point
        int enemyPoint = spawnList[spawnIndex].point;

        //Enemy Spawn
        GameObject enemy = objManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        //Player Attack Ready
        enemyLogic.player = player;
        //Managers Ready
        enemyLogic.objManager = objManager;
        enemyLogic.gameManager = this;

        //Enemy Move Logic
        if (enemyPoint == 5 || enemyPoint == 7)          //Right Spawn
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else if (enemyPoint == 6 || enemyPoint == 8)    //Left Spawn
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else                                        //Front Spawn
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }

        //ReSpawn Index on the rise
        spawnIndex++;
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        //Next ReSpawn Delay UpData
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }

    //Player Life
    public void UpdateLifeIcon(int life)
    {
        //UI Life Init Disable
        for (int index = 0; index < 3; index++)
        {
            lifeImg[index].color = new Color(1, 1, 1, 0);
        }

        //UI Life Active
        for (int index = 0; index < life; index++)
        {
            lifeImg[index].color = new Color(1, 1, 1, 1);
        }
    }

    //Player Respawn
    public void InputRespawnPlayer()
    {
        Invoke("RespawnPlayer", 2f);
    }

    public void RespawnPlayer()
    {
        player.transform.position = Vector3.down * 3.5f;
        player.SetActive(true);

        Player playerLogic = player.GetComponent<Player>();
        playerLogic.isHit = false;
    }

    //Explosion
    public void CallExplosion(Vector3 pos, string type)
    {
        GameObject explosion = objManager.MakeObj("Explosion");
        Explosion explosionLogic = explosion.GetComponent<Explosion>();

        explosion.transform.position = pos;
        explosionLogic.StartExplosion(type);
    }

    //Player GameOver
    public void GameOver()
    {
        SaveDataManager saveData = GameObject.Find("SaveDataManager").GetComponent<SaveDataManager>();
        gameOverSet.SetActive(true);
    }

    //Move Main
    public void MoveMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    //Game ReSet
    public void GameRetry()
    {
        SceneManager.LoadScene("Loading");
    }
}
