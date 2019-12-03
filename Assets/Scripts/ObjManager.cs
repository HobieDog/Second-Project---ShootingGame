﻿using System.Collections;
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

    //MakeObj
    GameObject[] targetPool;

    void Awake()
    {
        enemyL = new GameObject[10];
        enemyM = new GameObject[10];
        enemyS = new GameObject[10];

        itemBoom = new GameObject[10];
        itemPower = new GameObject[10];
        itemSpeed = new GameObject[10];
        itemCoin = new GameObject[20];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletEnemyA = new GameObject[100];
        bulletEnemyB = new GameObject[100];

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
        }

        return targetPool;
    }
}
