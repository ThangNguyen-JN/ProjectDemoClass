using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;
    public Animator animator;
    private Rigidbody2D rb;
    public float life = 5f;
    public float destroyDelay = 2f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(bullet, life);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetTrigger("explosion");
        Destroy(bullet,destroyDelay);
    }
}
