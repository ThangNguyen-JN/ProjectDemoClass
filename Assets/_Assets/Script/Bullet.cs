using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;
    public Animator animator;
    private Rigidbody2D rb;
    public float life = 2f;
    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
        Destroy(bullet, life);
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        animator.SetBool("explosion", true);
    }
}
