using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float moveSpeed = 7f;
    private Rigidbody2D rb;
    private Animator animator;

    public Vector3 moveInput;

    public bool isMoving = false;
    public bool isCrouch = false;
    //public bool isAttack = false;
    //public bool isDef = false;

    public float jumpForce = 3f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        MovePlayer();

        AttackPlayer();
        DefPlayer();
        CrouchPlayer();
        StrikePlayer();
    }

    public void MovePlayer()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");

        transform.position += moveInput * moveSpeed * Time.deltaTime;


        if (moveInput.x != 0)
        {
            isMoving = true;
            if (moveInput.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 0);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 0);
            }

        }
        else
        {
            isMoving = false;
        }
        animator.SetBool("isRuning", isMoving);

    }

    public void JumpPlayer()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.6f, 0.06f), CapsuleDirection2D.Horizontal, 0, groundLayer);
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == false) 
        {
            rb.AddForce(moveInput * jumpForce);
        }
    }

    public void AttackPlayer()
    {
        if (Input.GetKeyDown(KeyCode.J) && isCrouch == false )
        {
            moveSpeed = 0f;
            animator.SetTrigger("attack");
        }
        moveSpeed = 7f;

    }

    public void DefPlayer()
    {
        if (Input.GetKey(KeyCode.K) && moveInput.x == 0)
        {
            
            animator.SetBool("def", true);
        }
        else
        {
            animator.SetBool("def", false);
        }
    }

    public void CrouchPlayer()
    {
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("crouch", true);
            isCrouch = true;
            if(Input.GetKeyDown(KeyCode.J) && isCrouch == true)
            {
                animator.SetTrigger("dash");
                isCrouch= true;
            }
        }    
        else
        {
            animator.SetBool("crouch", false);
            isCrouch= false;
        }
    }

    public void StrikePlayer()
    {
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            animator.SetTrigger("strike");
        }
    }

    public void HurtPlayer()
    {

    }
}
