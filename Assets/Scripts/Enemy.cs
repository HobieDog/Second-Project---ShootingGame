using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Enemy Setting
    public string enemyName;
    public float speed;
    public int health;
    public Sprite[] sprites;

    //Enemy UI Setting
    public int enemyScore;

    //Bullet
    public GameObject BulletObjA;
    public GameObject BulletObjB;

    //Item
    public GameObject ItemObjBoom;
    public GameObject ItemObjPower;
    public GameObject ItemObjSpeed;
    public GameObject ItemObjCoin;

    //Bullet Setting
    public float maxShotDelay;
    public float curShotDelay;

    SpriteRenderer spriteRenderer;

    public ObjManager objManager;
    public GameObject player;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        switch (enemyName)
        {
            case "L":
                health = 40;
                break;
            case "M":
                health = 20;
                break;
            case "S":
                health = 10;
                break;
        }
    }

    void Update()
    {
        Launch();
        Reload();
    }

    void Launch()
    {
        //Reload Delay Time
        if (curShotDelay < maxShotDelay)
            return;

       if(enemyName == "S")
        {
            GameObject bulletS = objManager.MakeObj("BulletEnemyA");
            bulletS.transform.position = transform.position;
            Rigidbody2D rigid = bulletS.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        }
       else if (enemyName == "M")
        {
            GameObject bulletM = objManager.MakeObj("BulletEnemyA");
            bulletM.transform.position = transform.position;
            Rigidbody2D rigid = bulletM.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        }
       else if(enemyName == "L")
        {
            GameObject bulletR = objManager.MakeObj("BulletEnemyB");
            GameObject bulletL = objManager.MakeObj("BulletEnemyB");

            bulletR.transform.position = transform.position + Vector3.right * 0.35f + Vector3.down * 0.5f;
            bulletL.transform.position = transform.position + Vector3.left * 0.35f + Vector3.down * 0.5f;

            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            rigidR.AddForce(dirVecR.normalized * 3, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 3, ForceMode2D.Impulse);
        }

        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    public void OnHit(int damage)
    {
        if (health <= 0)
            return;

        health -= damage;
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);

        if (health <= 0)
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;

            //Random Ratio Item Drop
            int ran = Random.Range(0, 15);
            if(ran < 9)
            {
                Debug.Log("Not Item");
            }
            else if(ran < 10) //Boom
            {
                GameObject itemBoom = objManager.MakeObj("ItemBoom");
                itemBoom.transform.position = transform.position;
            }
            else if (ran < 11) //Power
            {
                GameObject itemPower = objManager.MakeObj("ItemPower");
                itemPower.transform.position = transform.position;
            }
            else if (ran < 12) //Speed
            {
                GameObject itemSpeed = objManager.MakeObj("ItemSpeed");
                itemSpeed.transform.position = transform.position;
            }
            else if (ran < 15) //Coin
            {
                GameObject itemCoin = objManager.MakeObj("ItemCoin");
                itemCoin.transform.position = transform.position;
            }

            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.damage);

            collision.gameObject.SetActive(false);
        }

    }
}
