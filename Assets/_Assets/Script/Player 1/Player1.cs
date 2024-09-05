using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player1 : MonoBehaviour
{
    public float moveSpeed = 7f;
    public Vector3 moveInput;

    public float moveDash = 15f;
    public float dashDuration = 0.2f;
    public GameObject dashWindPrefab;
    public Transform dashWindPoint;

    public float moveStrike = 15f;
    public float strikeDuration = 0.2f;

    private Rigidbody2D rb;
    private Animator animator;

    private bool isMoving = false;
    private bool isCrouch = false;
    private bool isJumping = false;


    public float jumpPower = 3f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;
    public float fallJump = 2f;
    Vector2 vecGravity;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 5f;
    Vector2 shootDirection;
    public float delayBeforeShooting = 1f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.5f, 0.08f), CapsuleDirection2D.Horizontal, 0, groundLayer);

        MovePlayer();
        JumpPlayer();
        AttackPlayer();
        DefPlayer();
        CrouchPlayer();
        DashPlayer();
        StrikePlayer();
        CastPlayer();
    }

    public void MovePlayer()
    {
        if (Input.GetKey(KeyCode.A))
        {
            moveInput.x = -1;
        }

        else if (Input.GetKey(KeyCode.D)) 
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

    public void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.velocity = new Vector2(moveInput.x, jumpPower);
            animator.SetBool("jumping", true);
            isGrounded = false;
            isJumping = true;
            isMoving = true;

        }
        else
        {
            animator.SetBool("jumping", false);
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecGravity * fallJump * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.J) && isGrounded == false && isJumping == true)
        {

            animator.SetTrigger("jumpAttack");
            Debug.Log("JumpAttack");

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
        if (Input.GetKey(KeyCode.S) && isGrounded)
        {
            animator.SetBool("crouch", true);
            isCrouch = true;
        }
        else
        {
            animator.SetBool("crouch", false);
            isCrouch = false;
        }
    }

    public void DashPlayer()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && isMoving == true)
        {
            
            animator.SetTrigger("dash");
            StartCoroutine(Dash());
            GameObject dashWind = Instantiate(dashWindPrefab, dashWindPoint.position, Quaternion.identity, transform);
            Destroy(dashWind, 0.45f);
            
        }
        else
        {
            animator.ResetTrigger("dash");
            animator.SetBool("isRuning", isMoving);
        }
    }

    private IEnumerator Dash()
    {
        float originalSpeed = moveSpeed;
        moveSpeed = moveDash;

        yield return new WaitForSeconds(dashDuration);

        moveSpeed = originalSpeed;
    }


    public void StrikePlayer()
    {
        if (Input.GetKeyDown(KeyCode.L) && isGrounded == true && isMoving == true)
        {
            animator.SetTrigger("strike");
            StartCoroutine(Strike());
        }
        else
        {
            animator.ResetTrigger("strike");
        }
    }

    private IEnumerator Strike()
    {
        float originalSpeed = moveSpeed;
        moveSpeed = moveStrike;

        yield return new WaitForSeconds(strikeDuration);

        moveSpeed = originalSpeed;
    }

    public void HurtPlayer()
    {

    }

    public void CastPlayer()
    {
        if (Input.GetKeyDown(KeyCode.H) && isGrounded == true && isCrouch == false)
        {
            animator.SetTrigger("cast");
            StartCoroutine(DelayedShooting());
        }
    }


    private IEnumerator DelayedShooting()
    {
        yield return new WaitForSeconds(delayBeforeShooting);
        Shooting();
    }

    public void Shooting()
    {
        UpdateShootDirection();
        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection.normalized * bulletForce;
        bullet.transform.localScale = new Vector3(shootDirection.x, 1, 1);

    }

    private void UpdateShootDirection()
    {
        if (transform.localScale.x > 0)
        {
            shootDirection = Vector2.right;
        }
        else
        {
            shootDirection = Vector2.left;
        }
    }
}
