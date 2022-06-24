using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator animator;
    private Transform check;

    private float xAxis;
    //private bool isJumped;
    private int jumpCount;

    private float jumpCoff;
    private float moveCoff;

    private bool isGrounded;
    private bool isGroundedPrev;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //animator = transform.Find("Sprite").GetComponent<Animator>();
        check = transform.Find("Check");

        xAxis = 0;
        //isJumped = false;
        jumpCount = 2;
        

        jumpCoff = 35;
        moveCoff = 200f;

        isGrounded = false;
        isGroundedPrev = false;
    }

    private void FixedUpdate()
    {
        SetVelocity();
        SetLocalScale();
        SetVelocity();
        SetLocalScale();
        isCheckGrounded();
        isCheckLanded();
    }

    private void isCheckGrounded()
    {
        isGroundedPrev = isGrounded;
        isGrounded = false;

        Collider2D[] c2ds = Physics2D.OverlapBoxAll(check.position, new Vector2(2, 0.1f), 0);
        foreach(Collider2D c2d in c2ds)
        {
            if(c2d.gameObject.layer == 7)
            {
                isGrounded = true;
                break;
            }
        }
    }

    private void isCheckLanded()
    {
        if(isGrounded == true && isGroundedPrev == false && jumpCount <= 1)
        {
            jumpCount = 2;
            //animator.SetTrigger("Idle");
        }
    }

    private void SetVelocity()
    {
        rb2d.AddForce(new Vector2(xAxis * moveCoff, 0));

        float x = Mathf.Clamp(rb2d.velocity.x, -6, 6);
        float y = Mathf.Clamp(rb2d.velocity.y, -35, 35);

        rb2d.velocity = new Vector2(x, y);
    }

    private void SetLocalScale()
    {
        if(xAxis == 0)
        {
            return;   
        }
        transform.localScale = new Vector3(xAxis > 0 ? 1 : -1, 1, 1);
    }

    
    void Update()
    {
        ActionMove();
        ActionJump();
    }

    private void ActionMove()
    {
        xAxis = Input.GetAxis("Horizontal");

        if(Mathf.Abs(xAxis) < 0.1f)
        {
            xAxis = 0;
            //animator.SetBool("IsWalk", false);
        }

        else
        {
            //animator.SetBool("IsWalk", true);
        }
    }

    private void ActionJump()
    {
        if(jumpCount == 0)
        {
            return;
        }
        else if(Input.GetButtonDown("Jump") == false)
        {
            return;
        }

        jumpCount--;
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpCoff);
        //animator.SetTrigger("Jump");
    }
}
