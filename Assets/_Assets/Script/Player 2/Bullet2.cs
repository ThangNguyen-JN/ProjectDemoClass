using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    public GameObject bullet;
    private Rigidbody2D rb;
    private Animator animator;
    public float life = 2f;
    public float delayDestroy = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Destroy(bullet, life);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetTrigger("explosion");
        Destroy(bullet, delayDestroy);
    }
}
