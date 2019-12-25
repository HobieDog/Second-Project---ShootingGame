using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        Invoke("Disable", 2f);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    public void StartExplosion(string target)
    {
        anim.SetTrigger("OnExplosion");

        switch (target)
        {
            //Boss
            case "B":
                transform.localScale = Vector3.one * 3f;
                break;
            //Player & Enemy L
            case "P":
            case "L":
                transform.localScale = Vector3.one * 2f;
                break;
            //Enemy M
            case "M":
                transform.localScale = Vector3.one * 1f;
                break;
            //Enemy S
            case "S":
                transform.localScale = Vector3.one * 0.6f;
                break;
        }
    }
}
