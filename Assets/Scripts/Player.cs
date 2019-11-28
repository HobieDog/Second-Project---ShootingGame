using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player Setting
    public float speed;
    public float power;
    public bool isTriggerTop;
    public bool isTriggerBottom;
    public bool isTriggerRight;
    public bool isTriggerLeft;

    //Player UI Setting
    public int score;
    public int life;

    //Bullet
    public GameObject BulletObjA;
    public GameObject BulletObjB;

    //Bullet Setting
    public float maxShotDelay;
    public float curShotDelay;

    public GameManager manager;
    public bool isHit;

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
                GameObject bullet = Instantiate(BulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletR = Instantiate(BulletObjA, transform.position + Vector3.right * 0.1f, transform.rotation);
                GameObject bulletL = Instantiate(BulletObjA, transform.position + Vector3.left * 0.1f, transform.rotation);
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletB = Instantiate(BulletObjB, transform.position, transform.rotation);
                Rigidbody2D rigidB = bulletB.GetComponent<Rigidbody2D>();
                rigidB.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 4:
                GameObject bulletBB = Instantiate(BulletObjB, transform.position + Vector3.up * 0.3f, transform.rotation);
                GameObject bulletC = Instantiate(BulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigidBB = bulletBB.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidC = bulletC.GetComponent<Rigidbody2D>();
                rigidBB.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 5:
                GameObject bulletBBB = Instantiate(BulletObjB, transform.position + Vector3.up * 0.2f, transform.rotation);
                GameObject bulletRR = Instantiate(BulletObjA, transform.position + Vector3.right * 0.25f, transform.rotation);
                GameObject bulletLL = Instantiate(BulletObjA, transform.position + Vector3.left * 0.25f, transform.rotation);
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
        if(collision.gameObject.tag == "Border")
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
            {
                life--;
                manager.UpdateLifeIcon(life);
            }
            

            //GameOver Check
            if(life == 0)
            {
                manager.GameOver();
            }
            else
            {
                manager.InputRespawnPlayer();
            }

            
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
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
