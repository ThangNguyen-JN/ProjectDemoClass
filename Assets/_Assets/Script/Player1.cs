using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;

    public Vector3 moveInput;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Attack();
    }

    public void MovePlayer()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        transform.position += moveInput * moveSpeed * Time.deltaTime;
        bool isMoving = moveInput.x != 0;
        animator.SetBool("isRuning", isMoving);

        if(moveInput.x != 0) 
        { 
            if (moveInput.x > 0)
            {
                transform.localScale = new Vector3 (1,1,0);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 0);
            }
        }
        
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("attack");
        }
    }
}
