using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    //Spawn Delay Setting
    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;

    //GameManager UI Setting
    public Text scoreText;
    public Text boomText;
    public Image[] lifeImg;
    public Image boomImg;
    public GameObject gameOverSet;

    void Update()
    {
        //Spawn Delay
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            //Random Spawn Time
            maxSpawnDelay = Random.Range(0.5f, 2.5f);
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
        //Random Spawn Enemy
        int ranEnemy = Random.Range(0, 3);
        //Random Spawn Point
        int ranPoint = Random.Range(0, 9);
        //Enemy Instance
        GameObject enemy = Instantiate(enemyObjs[ranEnemy], 
                                       spawnPoints[ranPoint].position,
                                       spawnPoints[ranPoint].rotation);
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        //Player Attack Ready
        enemyLogic.player = player;

        //Enemy Move Logic
        if(ranPoint == 6 || ranPoint == 8)          //Right Spawn
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else if (ranPoint == 5 || ranPoint == 7)    //Left Spawn
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else                                        //Front Spawn
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }
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

    //Player GameOver
    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }

    //Game ReSet
    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }
}
