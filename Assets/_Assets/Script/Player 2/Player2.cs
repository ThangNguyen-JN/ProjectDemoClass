using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector3 moveInput;
    private bool isMoving;

    private Rigidbody2D rb;
    private Animator animator;

    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;

    public float fallJump;
    Vector2 vecGravity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        JumpPlayer();
        AttackPlayer();
        CrouchPlayer();
        StrikePlayer();
        FlyKickPlayer();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.5f, 0.05f), CapsuleDirection2D.Horizontal, 0 , groundLayer);
    }

    public void MovePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput.x = -1;
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveInput.x = 1;
        }

        else
        {
            moveInput.x = 0;
        }

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
       animator.SetFloat("speed", moveInput.sqrMagnitude);

    }

    public void AttackPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) /*&& isCrouch == false && isGrounded*/)
        {
            animator.SetTrigger("attack");
        }
    }

    public void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(moveInput.x, jumpForce);
            isGrounded = false;

        }

        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecGravity * fallJump * Time.deltaTime;
        }    
    }

    public void CrouchPlayer()
    {
        if (Input.GetKey(KeyCode.DownArrow) && isGrounded)
        {
            animator.SetBool("crouch", true);
        }
        
    }

    public void StrikePlayer()
    {

    }

    public void FlyKickPlayer()
    {

    }

    
    
}
