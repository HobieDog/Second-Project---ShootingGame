using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player Setting
    public float speed;
    public float maxSpeed;
    public int power;
    public int maxPower;

    //Border Player
    public bool isTriggerTop;
    public bool isTriggerBottom;
    public bool isTriggerRight;
    public bool isTriggerLeft;

    //Player UI Setting
    public int coin;
    public int score;
    public int life;

    //Bullet
    public GameObject BulletObjA;
    public GameObject BulletObjB;

    //Boom
    public int boomCount;
    public int maxBoom;
    public GameObject BoomEffect;
    public bool isBoomTime;

    //Bullet Setting
    public float maxShotDelay;
    public float curShotDelay;

    //Hit Check
    public bool isHit;

    //Manager
    public GameManager gameManager;
    public ObjManager objManager;
   
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Launch();
        Reload();
        OnBoomEffect();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTriggerRight && h == 1) || (isTriggerLeft && h == -1))
            h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if ((isTriggerTop && v == 1) || (isTriggerBottom && v == -1))
            v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        //Move Animation
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }
    }

    void Launch()
    { 
        if (!Input.GetButton("Jump"))
            return;

        //Reload Delay Time
        if (curShotDelay < maxShotDelay)
            return;

        //Power Lever Bullet Style
        switch (power)
        {
            case 1:
                GameObject bullet = objManager.MakeObj("BulletPlayerA");
                bullet.transform.position = transform.position;
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletR = objManager.MakeObj("BulletPlayerA");
                GameObject bulletL = objManager.MakeObj("BulletPlayerA");
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletB = objManager.MakeObj("BulletPlayerB");
                bulletB.transform.position = transform.position;
                Rigidbody2D rigidB = bulletB.GetComponent<Rigidbody2D>();
                rigidB.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 4:
                GameObject bulletBB = objManager.MakeObj("BulletPlayerB");
                GameObject bulletC = objManager.MakeObj("BulletPlayerA");
                bulletBB.transform.position = transform.position + Vector3.up * 0.3f;
                bulletC.transform.position = transform.position;
                Rigidbody2D rigidBB = bulletBB.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidC = bulletC.GetComponent<Rigidbody2D>();
                rigidBB.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 5:
                GameObject bulletBBB = objManager.MakeObj("BulletPlayerB");
                GameObject bulletRR = objManager.MakeObj("BulletPlayerA");
                GameObject bulletLL = objManager.MakeObj("BulletPlayerA");
                bulletBBB.transform.position = transform.position + Vector3.up * 0.2f;
                bulletRR.transform.position = transform.position + Vector3.right * 0.25f;
                bulletLL.transform.position = transform.position + Vector3.left * 0.25f;
                Rigidbody2D rigidBBB = bulletBBB.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                rigidBBB.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
        }
        
        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        SaveDataManager saveData = GameObject.Find("SaveDataManager").GetComponent<SaveDataManager>();
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTriggerTop = true;
                    break;
                case "Bottom":
                    isTriggerBottom = true;
                    break;
                case "Right":
                    isTriggerRight = true;
                    break;
                case "Left":
                    isTriggerLeft = true;
                    break;
            }
        }

        else if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            //Player Life Update
            if (isHit)
                return;

            isHit = true;
            life--;
            gameManager.UpdateLifeIcon(life);

            //GameOver Check
            if(life == 0)
            {
                gameManager.GameOver();
            }
            else
            {
                gameManager.InputRespawnPlayer();
            }

            
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }

        else if(collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();

            switch (item.type)
            {
                case "Boom":
                    if (boomCount == maxBoom)
                        score += 500;
                    else
                        boomCount++;
                    break;
                case "Power":
                    if(power == maxPower)
                        score += 500;
                    else
                        power++;
                    break;
                case "Speed":
                    if (speed == maxSpeed)
                        score += 500;
                    else
                        speed++;
                    break;
                case "Coin":
                    //いつかmoneyで変えてShopを追加する予定。
                    score += 1000;
                    saveData.stageCoin += 1000;
                    break;
            }
            collision.gameObject.SetActive(false); ;
        }
    }

    void OnBoomEffect()
    {
        if (!Input.GetButton("Fire1"))
            return;

        if (isBoomTime)
            return;

        if (boomCount == 0)
            return;

        boomCount--;
        isBoomTime = true;

        //Effect visible
        BoomEffect.SetActive(true);
        Invoke("OffBoomEffect", 3.5f);

        //Remove Enemy
        GameObject[] enemiesL = objManager.GetPool("EnemyL");
        GameObject[] enemiesM = objManager.GetPool("EnemyM");
        GameObject[] enemiesS = objManager.GetPool("EnemyS");

        for (int i = 0; i < enemiesL.Length; i++)
        {
            if (enemiesL[i].activeSelf)
            {
                Enemy enemyLogic = enemiesL[i].GetComponent<Enemy>();
                enemyLogic.OnHit(300);
            }
        }

        for (int i = 0; i < enemiesM.Length; i++)
        {
            if (enemiesM[i].activeSelf)
            {
                Enemy enemyLogic = enemiesM[i].GetComponent<Enemy>();
                enemyLogic.OnHit(300);
            }
        }

        for (int i = 0; i < enemiesS.Length; i++)
        {
            if (enemiesS[i].activeSelf)
            {
                Enemy enemyLogic = enemiesS[i].GetComponent<Enemy>();
                enemyLogic.OnHit(300);
            }
        }

        //Remove Enemy Bullet
        GameObject[] bulletsA = objManager.GetPool("BulletEnemyA");
        GameObject[] bulletsB = objManager.GetPool("BulletEnemyB");

        for (int i = 0; i < bulletsA.Length; i++)
        {
            if (bulletsA[i].activeSelf)
            {
                bulletsA[i].SetActive(false);
            }
        }

        for (int i = 0; i < bulletsB.Length; i++)
        {
            if (bulletsB[i].activeSelf)
            {
                bulletsB[i].SetActive(false);
            }
        }
    }

    void OffBoomEffect()
    {
        BoomEffect.SetActive(false);
        isBoomTime = false;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTriggerTop = false;
                    break;
                case "Bottom":
                    isTriggerBottom = false;
                    break;
                case "Right":
                    isTriggerRight = false;
                    break;
                case "Left":
                    isTriggerLeft = false;
                    break;
            }
        }
    }
}
