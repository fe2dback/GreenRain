using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public static Rigidbody2D rb2d;
    private Animator animator;
    private Transform check;
    public Transform WallCheck;
    private Transform interaction;

    private float xAxis;
    //private bool isJumped;
    public int jumpCount;

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

    public bool isWall;
    public float slidingSpeed;
    public float wallJumpPower;
    public bool isWallJump;

    private bool isinteraction;
    private bool checkinteraction;
    public static bool interactionhas;

    public bool isControl;

    public float criPer;
    public float criDmg;
    private bool criCheck;

    public GameObject bullet;
    public Transform SkillPos;

    public float skillCoolTime;
    private float skillCurTime;



    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = transform.Find("Sprite").GetComponent<Animator>();
        check = transform.Find("Check");
        interaction = transform.Find("Interaction");
        //WallCheck = transform.Find("WallCheck");
        //pos = transform.Find("AttackCheck");

        xAxis = 0;
        //isJumped = false;
        jumpCount = 2;
        

        jumpCoff = 35f;
        moveCoff = 200f;
        speed = 400f;
        isGrounded = false;
        isGroundedPrev = false;

        curTime = 0;
        //coolTime = 0.5f;

        hit = false;
        isRight = 1;
        isWallJump = false;

        isinteraction = false;
        checkinteraction = false;
        interactionhas = false;

        isControl = true;

        criPer = 0;
        criDmg = 0;
        criCheck = false;

        skillCurTime = 0;

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
        isCheckInteraction();
        CheckInteraction();

    }

    

    private void isCheckInteraction()
    {
        checkinteraction = isinteraction;
        isinteraction = false;
        Collider2D[] ctd = Physics2D.OverlapBoxAll(interaction.position, new Vector2(3, 3), 0);
        foreach (Collider2D ctdd in ctd)
        {
            if (ctdd.gameObject.layer == 9)
            {
                isinteraction = true;
                break;
            }

        }
    }
    private void CheckInteraction()
    {
        if (checkinteraction == true)
        {
            if (Input.GetKey(KeyCode.F))
            {
                interactionhas = true;
            }
        }
    }

    private void WallSliding()
    {
        if(isWall)
        {
            isWallJump = false;
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * slidingSpeed);

            if(Input.GetAxis("Jump") != 0)
            {
                isWallJump = true;

                Invoke("FreezX", 1);
                rb2d.velocity = new Vector2(-transform.localScale.x * wallJumpPower, 0.5f * wallJumpPower);
                //animator.SetTrigger("Jump");
                FlipPlayer();
                jumpCount--;
            }
        }
    }
    
    private void FreezX()
    {
        isWallJump = false;
    }
    
    private void FlipPlayer()
    {
        //transform.eulerAngles = new Vector3(0, Mathf.Abs(transform.eulerAngles.y - 180), 0);
        //isRight = isRight * -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
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
            else if(c2d.gameObject.layer == 11)//enemy2
            {
                PlayerDamage(1);
                isGrounded = true;
                animator.SetTrigger("Idle");
                jumpCount = 2;
                break;
            }
            else if (c2d.gameObject.layer == 8)//enemy
            {
                PlayerDamage(1);
                isGrounded = true;
                animator.SetTrigger("Idle");
                jumpCount = 2;
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
        if(isControl == true)
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
        else
        {
            float x = rb2d.velocity.x;
            float y = Mathf.Clamp(rb2d.velocity.y, -35, 35);


            rb2d.velocity = new Vector2(x, y);
            return;
        }

       
    }

    private void SetLocalScale()
    {
        if(isControl == true)
        {
            if (isWallJump == true)
            {
                return;
            }

            if (xAxis == 0)
            {
                return;
            }

            FlipPlayer();
            transform.localScale = new Vector3(xAxis > 0 ? 1 : -1, 1, 1);

        }
        else
        {
            return;
        }
        
    }

    
    void Update()
    {
        ActionMove();
        ActionJump();
        playerAttack();
        WallJumpCheck();
        PlayerSkill();
        
    }

    private void ActionMove()
    {
        /*
        if(hit == true)
        {
            return;
        }
        */

        if (isWallJump == true)
        {
            return;
        }
            
        if(isControl == true)
        {
            xAxis = Input.GetAxis("Horizontal");

            if (Mathf.Abs(xAxis) < 0.1f)
            {
                xAxis = 0;
                animator.SetBool("IsWalk", false);
            }

            else
            {
                animator.SetBool("IsWalk", true);
            }
        }
        
    }

    private void ActionJump()
    {

        if(isControl == true)
        {
            jumpCoff = 35f;
            if (jumpCount == 0)
            {
                return;
            }
            else if (Input.GetButtonDown("Jump") == false)
            {
                return;
            }
            
            jumpCount--;
            
            
            if (jumpCount == 1)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpCoff);
                animator.SetTrigger("Jump");
                
            }
            else if (jumpCount == 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpCoff);
                animator.SetTrigger("Jump_two");
            }
            else
            {
                return;
            }


            
            
            
        }
        
        

    }
    private void  playerAttack()
    {

        if(isControl == true)
        {
            if (curTime <= 0)
            {
                if (Input.GetMouseButtonDown(0) && jumpCount == 2)
                {
                    Collider2D[] c2ds1 = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                    foreach (Collider2D c2d1 in c2ds1)
                    {
                        criPer = Random.Range(1, 101);
                        criCheck = (0 == (int)criPer % 5);

                        if (c2d1.tag == "Enemy")
                        {
                            if(criCheck == true)
                            {
                                criDmg = 2;
                                c2d1.GetComponent<EnemyMain>().TakeDamage(Random.Range(200 * (int)criDmg, 299 * (int)criDmg));
                                continue;
                            }
                            c2d1.GetComponent<EnemyMain>().TakeDamage(Random.Range(200,299));

                            //Debug.Log("10");
                        }
                        else if(c2d1.tag == "Enemy2")
                        {
                            if (criCheck == true)
                            {
                                criDmg = 2;
                                c2d1.GetComponent<Enemy2Main>().TakeDamage(Random.Range(100 * (int)criDmg, 199 * (int)criDmg));
                                continue;
                            }
                            c2d1.GetComponent<Enemy2Main>().TakeDamage(Random.Range(100,199));
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
        else
        {
            return;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(WallCheck.position, Vector2.right * transform.localScale.x * wallCheckDistance);
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
            float x = Mathf.Clamp(rb2d.velocity.x, -10, 10) ;
            float y = rb2d.velocity.y;

            rb2d.velocity = new Vector2(x, y);

            rb2d.velocity += new Vector2(-20f, y);
            animator.SetTrigger("Hit");
            hit = false;
            StartCoroutine(hittingnomove());
        }
        
    }

    private void WallJumpCheck()
    {
        
        isWall = Physics2D.Raycast(WallCheck.position, Vector2.right * transform.localScale.x, wallCheckDistance, w_Layer);
        
    }

    private IEnumerator hittingnomove()
    {
        isControl = false;
        if(isControl == false)
        {
            jumpCount = 2;
        }
        yield return new WaitForSeconds(0.5f);
        
        isControl = true;
    }

    private void PlayerSkill()
    {
        if(isControl == true)
        {
            if (skillCurTime <= 0)
            {
                //skillCurTime = 0;
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if(transform.localScale.x == -1)
                    {
                        Instantiate(bullet, pos.position, Quaternion.Euler(0, 180, 0));
                    }
                    else
                    {
                        Instantiate(bullet, pos.position, Quaternion.Euler(0, 0, 0));
                    }
                    
                    skillCurTime = skillCoolTime;
                }
                
            }
            else
            {
                PlayerBullet.attackCheck = false;
                skillCurTime -= Time.deltaTime;
            }
            
        }
        else
        {
            return;
        }
        
    }

    

    
}
