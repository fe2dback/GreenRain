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
    private float speed;

    private bool isGrounded;
    private bool isGroundedPrev;

    private float curTime;
    private float coolTime;

    Transform pos;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = transform.Find("Sprite").GetComponent<Animator>();
        check = transform.Find("Check");
        pos = transform.Find("AttackCheck");

        xAxis = 0;
        //isJumped = false;
        jumpCount = 2;
        

        jumpCoff = 35;
        moveCoff = 200f;
        speed = 400f;
        isGrounded = false;
        isGroundedPrev = false;

        curTime = 0;
        coolTime = 0.5f;
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
            animator.SetTrigger("Idle");
        }
    }

    private void SetVelocity()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Run", true);
            if (isGrounded == false)
            {
                moveCoff = 200f;
                
            }
            else
            {
                moveCoff = speed;
            }
        }
        
        else
        {
            moveCoff = 200f;
            animator.SetBool("Run", false);
        }


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
        playerAttack();
    }

    private void ActionMove()
    {
        xAxis = Input.GetAxis("Horizontal");

        if(Mathf.Abs(xAxis) < 0.1f)
        {
            xAxis = 0;
            animator.SetBool("IsWalk", false);
        }

        else
        {
            animator.SetBool("IsWalk", true);
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
        if(jumpCount == 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpCoff);
            animator.SetTrigger("Jump_two");
        }
        else if(jumpCount == 1)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpCoff);
            animator.SetTrigger("Jump");
        }

    }
    void playerAttack()
    {

        if (curTime <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D[] c2ds1 = Physics2D.OverlapBoxAll(pos.position, new Vector2(2, 3), 0);
                foreach(Collider2D c2d1 in c2ds1)
                {
                    
                    
                     Debug.Log(c2d1.tag);
                    
                    
                }
                
                animator.SetTrigger("Attack");

                curTime = coolTime;
            }

        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }
    
}
