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


    // Start is called before the first frame update
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
}
