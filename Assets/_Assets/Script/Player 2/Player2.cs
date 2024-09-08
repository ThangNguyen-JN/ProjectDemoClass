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

    public float moveStrike = 12f;
    public float strikeDuration = 0.2f;

    public float moveFlyKick = 12f;
    public float flyKickDuration = 0.2f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 10f;
    Vector2 shootDirection;
    public float delayBullet = 0.2f;

    public float delayAttack = 0.5f;


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
            StartCoroutine(ShootingDelay());
        }
    }

    public IEnumerator ShootingDelay()
    {
        yield return new WaitForSeconds(delayBullet);
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
        if (Input.GetKey(KeyCode.DownArrow) && isGrounded )
        {
            animator.SetBool("crouch", true);
        }
        else
        {
            animator.SetBool("crouch", false);
        }
    }

    public void StrikePlayer()
    {
        if (Input.GetKeyDown(KeyCode.Keypad2) && isGrounded && isMoving == true)
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

    public void FlyKickPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Keypad3) && isGrounded && isMoving == true)
        {
            animator.SetTrigger("flyKick");
            StartCoroutine(FlyKick());
        }

        else
        {
            animator.ResetTrigger("flyKick");
        }
    }

    private IEnumerator FlyKick()
    {
        float originalSpeed = moveSpeed;
        moveSpeed = moveFlyKick;
        yield return new WaitForSeconds(flyKickDuration);
        moveSpeed = originalSpeed;
    }

    
    
}
