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

    //Manager Setting
    public ObjManager objManager;
    public GameManager gameManager;

    public GameObject player;
    SpriteRenderer spriteRenderer;
    Animator anim;

    //Boss Pattern
    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(enemyName == "B")
            anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        switch (enemyName)
        {
            case "B":
                health = 3000;
                Invoke("Stop", 1.5f);
                break;
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

    void Stop()
    {
        //OnEnable Invoke Error Prevention
        if (!gameObject.activeSelf)
            return;

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Invoke("BossPattern", 2);
    }

    void BossPattern()
    {
        //Rotate Pattern
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;

        switch (patternIndex)
        {
            case 0:
                LaunchFoward();
                break;
            case 1:
                LaunchShot();
                break;
            case 2:
                LaunchArc();
                break;
            case 3:
                LaunchAround();
                break;
        }
    }

    //Boss Pattern 0
    void LaunchFoward()
    {
        //Launch 4 Bullet Foward
        GameObject bulletR = objManager.MakeObj("BulletBossB");
        GameObject bulletRR = objManager.MakeObj("BulletBossB");
        GameObject bulletL = objManager.MakeObj("BulletBossB");
        GameObject bulletLL = objManager.MakeObj("BulletBossB");

        bulletR.transform.position = transform.position + Vector3.right * 0.7f + Vector3.down * 0.5f;
        bulletRR.transform.position = transform.position + Vector3.right * 0.8f + Vector3.down * 0.5f;
        bulletL.transform.position = transform.position + Vector3.left * 0.7f + Vector3.down * 0.5f;
        bulletLL.transform.position = transform.position + Vector3.left * 0.8f + Vector3.down * 0.5f;

        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

        rigidR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);

        //Pattern Counting
        curPatternCount++;

        if(curPatternCount < maxPatternCount[patternIndex])
            Invoke("LaunchFoward", 2);
        else
            Invoke("BossPattern", 3);
    }

    //Boss Pattern 1
    void LaunchShot()
    {
        //Launch 5 Random Shotgun Bullet to Player
        for(int i = 0; i < 5; i++)
        {
            GameObject bullet = objManager.MakeObj("BulletEnemyB");
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 8, ForceMode2D.Impulse);
        }

        //Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("LaunchShot", 3.5f);
        else
            Invoke("BossPattern", 3);
    }

    //Boss Pattern 2
    void LaunchArc()
    {
        //Launch Arc Continue Launch
        GameObject bullet = objManager.MakeObj("BulletEnemyA");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 8 * curPatternCount/maxPatternCount[patternIndex]), -1);
        rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);

        //Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("LaunchArc", 0.15f);
        else
            Invoke("BossPattern", 3);
    }

    //Boss Pattern 3
    void LaunchAround()
    {
        //Launch Around
        int roundNumA = 50;
        int roundNumB = 40;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;

        for (int i = 0; i < roundNum; i++)
        {
            GameObject bullet = objManager.MakeObj("BulletBossA");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundNum)
                                        ,Mathf.Sin(Mathf.PI * 2 * i / roundNum));
            rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * i / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
        }
        

        //Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("LaunchAround", 0.5f);
        else
            Invoke("BossPattern", 3);
    }

    void Update()
    {
        if (enemyName == "B")
            return;
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

        //Boss Animetion
        if (enemyName == "B")
        {
            anim.SetTrigger("OnDamaged");
        }
        else
        {
            spriteRenderer.sprite = sprites[1];
            Invoke("ReturnSprite", 0.1f);
        }
        
        if (health <= 0)
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;

            //Random Ratio Item Drop(Boss = Not Item)
            int ran = enemyName == "B" ? 0 : Random.Range(0, 15);
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
            gameManager.CallExplosion(transform.position, enemyName);
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet" && enemyName != "B")
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
