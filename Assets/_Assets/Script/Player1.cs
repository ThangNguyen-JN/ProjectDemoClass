using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    public float jumpPower = 3f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;
    public float fallJump = 2f;
    Vector2 vecGravity;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 5f;
    Vector2 shootDirection = Vector2.right;





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
        DefPlayer();
        CrouchPlayer();
        StrikePlayer();
        CastPlayer();
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
        animator.SetFloat("speed", moveInput.sqrMagnitude);

    }

    public void JumpPlayer()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.5f, 0.08f), CapsuleDirection2D.Horizontal, 0, groundLayer);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.velocity = new Vector2(moveInput.x, jumpPower);
            animator.SetTrigger("jump");
            isGrounded = false;

        }

        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecGravity * fallJump * Time.deltaTime;
        }

        if (isGrounded == false )
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                animator.SetTrigger("jumpAttack");
            }
        }
        else
        {
            animator.ResetTrigger("jumpAttack");
        }

    }

    public void AttackPlayer()
    {
        if (Input.GetKeyDown(KeyCode.J) && isCrouch == false && isGrounded)
        {

            animator.SetTrigger("attack");
        }
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
        if (Input.GetKey(KeyCode.S) && isGrounded == true)
        {
            animator.SetBool("crouch", true);
            isCrouch = true;
            if (Input.GetKeyDown(KeyCode.J) && isCrouch == true)
            {
                animator.SetTrigger("dash");
            }
            else
            {
                animator.ResetTrigger("dash");
            }
        }
        else
        {
            animator.SetBool("crouch", false);
            isCrouch = false;
        }
    }

    public void StrikePlayer()
    {
        if (Input.GetKeyDown(KeyCode.L) && isGrounded == true)
        {
            animator.SetTrigger("strike");
        }
    }

    public void HurtPlayer()
    {

    }

    public void CastPlayer()
    {
        if (Input.GetKeyDown(KeyCode.H) && isGrounded == true)
        {
            animator.SetTrigger("cast");
            Shooting();
        }
    }

    public void Shooting()
    {
        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection.normalized * bulletForce;

    }
}
