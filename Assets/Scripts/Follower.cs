using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    //Bullet Setting
    public float maxShotDelay;
    public float curShotDelay;

    //Manager
    public ObjManager objManager;

    //Follow Logic Setting
    public int speed;
    public Vector3 followPos;
    public Transform parent;
    public Queue<Vector3> parentPos;  //Queue = FIFO (First Input First Output)
    public int followDelay;

    void Awake()
    {
        parentPos = new Queue<Vector3>();
    }

    void Update()
    {
        SearchParent();
        Follow();
        Launch();
        Reload();
    }

    void SearchParent()
    {
        //Input Position
        if(!parentPos.Contains(parent.position))
            parentPos.Enqueue(parent.position);

        //Output Position
        if (parentPos.Count > followDelay)
            followPos = parentPos.Dequeue();
        else if (parentPos.Count < followDelay)
            followPos = parent.position;
    }

    void Follow()
    {
        transform.position = followPos;
        //transform.RotateAround(followPos, Vector3.up, speed * Time.deltaTime);
    }

    void Launch()
    {
        //Reload Delay Time
        if (curShotDelay < maxShotDelay)
            return;

        GameObject bullet = objManager.MakeObj("bulletFollower");
        bullet.transform.position = transform.position;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        //Delay Time Initialize
        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
}
