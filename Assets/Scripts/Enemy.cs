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
    public GameObject ItemObjCoin;

    //Bullet Setting
    public float maxShotDelay;
    public float curShotDelay;

    SpriteRenderer spriteRenderer;

    public GameObject player;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            GameObject bullet = Instantiate(BulletObjA, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        }
       else if (enemyName == "M")
        {
            GameObject bullet = Instantiate(BulletObjA, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        }
       else if(enemyName == "L")
        {
            GameObject bulletR = Instantiate(BulletObjB, transform.position + Vector3.right * 0.35f + Vector3.down * 0.5f, transform.rotation);
            GameObject bulletL = Instantiate(BulletObjB, transform.position + Vector3.left * 0.35f + Vector3.down * 0.5f, transform.rotation);

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
            int ran = Random.Range(0, 10);
            if(ran < 5)
            {
                Debug.Log("Not Item");
            }
            else if(ran < 6) //Boom
            {
                Instantiate(ItemObjBoom, transform.position, ItemObjBoom.transform.rotation);
            }
            else if (ran < 8) //Power
            {
                Instantiate(ItemObjPower, transform.position, ItemObjPower.transform.rotation);
            }
            else if (ran < 10) //Coin
            {
                Instantiate(ItemObjCoin, transform.position, ItemObjCoin.transform.rotation);
            }

            Destroy(gameObject);
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet")
            Destroy(gameObject);
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.damage);

            Destroy(collision.gameObject);
        }

    }
}
