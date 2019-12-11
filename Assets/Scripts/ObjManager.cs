using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;

    public GameObject itemBoomPrefab;
    public GameObject itemPowerPrefab;
    public GameObject itemSpeedPrefab;
    public GameObject itemCoinPrefab;

    public GameObject bulletPlayerAPrefab;
    public GameObject bulletPlayerBPrefab;
    public GameObject bulletEnemyAPrefab;
    public GameObject bulletEnemyBPrefab;
    public GameObject bulletFollowerPrefab;

    //Enemy Type
    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;

    //Item Type
    GameObject[] itemBoom;
    GameObject[] itemPower;
    GameObject[] itemSpeed;
    GameObject[] itemCoin;

    //Bullet Type
    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;
    GameObject[] bulletFollower;

    //MakeObj
    GameObject[] targetPool;

    void Awake()
    {
        enemyL = new GameObject[20];
        enemyM = new GameObject[20];
        enemyS = new GameObject[20];

        itemBoom = new GameObject[20];
        itemPower = new GameObject[20];
        itemSpeed = new GameObject[20];
        itemCoin = new GameObject[40];

        bulletPlayerA = new GameObject[150];
        bulletPlayerB = new GameObject[150];
        bulletEnemyA = new GameObject[150];
        bulletEnemyB = new GameObject[150];
        bulletFollower = new GameObject[150];

        Generate();
    }

    void Generate()
    {
        //Enemy
        for(int i = 0; i < enemyL.Length; i++)
        {
            enemyL[i] = Instantiate(enemyLPrefab);
            enemyL[i].SetActive(false);
        }
        for (int i = 0; i < enemyM.Length; i++)
        {
            enemyM[i] = Instantiate(enemyMPrefab);
            enemyM[i].SetActive(false);
        }
        for (int i = 0; i < enemyS.Length; i++)
        {
            enemyS[i] = Instantiate(enemySPrefab);
            enemyS[i].SetActive(false);
        }

        //Item
        for (int i = 0; i < itemBoom.Length; i++)
        {
            itemBoom[i] = Instantiate(itemBoomPrefab);
            itemBoom[i].SetActive(false);
        }
        for (int i = 0; i < itemPower.Length; i++)
        {
            itemPower[i] = Instantiate(itemPowerPrefab);
            itemPower[i].SetActive(false);
        }
        for (int i = 0; i < itemSpeed.Length; i++)
        {
            itemSpeed[i] = Instantiate(itemSpeedPrefab);
            itemSpeed[i].SetActive(false);
        }
        for (int i = 0; i < itemCoin.Length; i++)
        {
            itemCoin[i] = Instantiate(itemCoinPrefab);
            itemCoin[i].SetActive(false);
        }

        //Bullet
        for (int i = 0; i < bulletPlayerA.Length; i++)
        {
            bulletPlayerA[i] = Instantiate(bulletPlayerAPrefab);
            bulletPlayerA[i].SetActive(false);
        }
        for (int i = 0; i < bulletPlayerB.Length; i++)
        {
            bulletPlayerB[i] = Instantiate(bulletPlayerBPrefab);
            bulletPlayerB[i].SetActive(false);
        }
        for (int i = 0; i < bulletEnemyA.Length; i++)
        {
            bulletEnemyA[i] = Instantiate(bulletEnemyAPrefab);
            bulletEnemyA[i].SetActive(false);
        }
        for (int i = 0; i < bulletEnemyB.Length; i++)
        {
            bulletEnemyB[i] = Instantiate(bulletEnemyBPrefab);
            bulletEnemyB[i].SetActive(false);
        }
        for (int i = 0; i < bulletFollower.Length; i++)
        {
            bulletFollower[i] = Instantiate(bulletFollowerPrefab);
            bulletFollower[i].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            //Enemy
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;

            //Item
            case "ItemBoom":
                targetPool = itemBoom;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemSpeed":
                targetPool = itemSpeed;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;

            //Bullet
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "bulletFollower":
                targetPool = bulletFollower;
                break;
        }

        //Obj Active
        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }

        return null;
    }

    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            //Enemy
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;

            //Item
            case "ItemBoom":
                targetPool = itemBoom;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemSpeed":
                targetPool = itemSpeed;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;

            //Bullet
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "bulletFollower":
                targetPool = bulletFollower;
                break;
        }

        return targetPool;
    }
}
