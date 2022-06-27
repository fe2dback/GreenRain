using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public static Rigidbody2D rb2d;
    private Animator animator;
    private Transform check;
    public Transform WallCheck;

    private float xAxis;
    //private bool isJumped;
    private int jumpCount;

    private float jumpCoff;
    private float moveCoff;
    private float speed;

    private bool isGrounded;
    private bool isGroundedPrev;

    private float curTime;
    public float coolTime;

    public Transform pos;
    public Vector2 boxSize;

    private bool isAttacked;
    private bool isAttackedPrev;

    public float wallCheckDistance;
    public LayerMask w_Layer;

    public int hp;
    public static bool hit;

    public float isRight; // 플레이어가 바로보든 방향 1= 오른쪽, -1는 왼쪽
    private bool isWall;
    public float slidingSpeed;

    public float wallJumpPower;

    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = transform.Find("Sprite").GetComponent<Animator>();
        check = transform.Find("Check");
        //WallCheck = transform.Find("WallCheck");
        //pos = transform.Find("AttackCheck");

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

        hit = false;
        isRight = 1;

        //isAttacked = false;
        //isAttackedPrev = false;
    }

    private void FixedUpdate()
    {
        SetVelocity();
        SetLocalScale();
        SetVelocity();
        SetLocalScale();
        isCheckGrounded();
        isCheckLanded();
        WallSliding();
        
    }

    private void WallSliding()
    {
        if(isWall == true)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * slidingSpeed);

            if(Input.GetAxis("Jump") != 0)
            {
                rb2d.velocity = new Vector2(-isRight * wallJumpPower, 0.9f * wallJumpPower);
            }
        }
    }
    private void FlipPlayer()
    {
        transform.eulerAngles = new Vector3(0, Mathf.Abs(transform.eulerAngles.y - 180), 0);
        isRight = isRight * -1;
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
        WallJumpCheck();
    }

    private void ActionMove()
    {
        /*
        if(hit == true)
        {
            return;
        }
        */

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
    private void  playerAttack()
    {

        if (curTime <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D[] c2ds1 = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach(Collider2D c2d1 in c2ds1)
                {

                    if (c2d1.tag == "Enemy")
                    {
                        c2d1.GetComponent<EnemyMain>().TakeDamage(1);
                                
                        //Debug.Log("10");
                    }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(WallCheck.position, Vector2.right * isRight * wallCheckDistance);
    }

    public void PlayerDamage(int damage)
    {
        hp -= damage;
        hit = true;

        if (hp <= 0)
        {
            Debug.Log("Player Dead");
        }
        else
        {
            float x = rb2d.velocity.x;
            float y = rb2d.velocity.y;

            rb2d.velocity += new Vector2(-20, y);
        }
    }

    private void WallJumpCheck()
    {
        isWall = Physics2D.Raycast(WallCheck.position, Vector2.right * isRight, wallCheckDistance, w_Layer);
    }

    
}
