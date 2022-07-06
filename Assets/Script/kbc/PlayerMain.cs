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
    public int spawnhp;
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
    public static bool CheckIsControl;

    public float criPer;
    public float criDmg;
    private bool criCheck;

    public GameObject bullet;
    public Transform SkillPos;

    public float skillCoolTime;
    private float skillCurTime;

    private string type;
    public float per;
    public float duration;
    public Sprite icon;
    //public List<BaseSkill> onBuff = new List<BaseSkill>();

    public static bool enemyCheck;
    public static bool enemy2Check;

    public static Vector3 enemyTp;
    public static Vector3 enemy2Tp;


    public AudioClip attackClip;
    public AudioClip skillClip;
    //public AudioClip walkClip;

    private AudioSource audioSrc;
    public bool isMoving;
    bool walkCheck;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = transform.Find("Sprite").GetComponent<Animator>();
        check = transform.Find("Check");
        interaction = transform.Find("Interaction");
        audioSrc = GetComponent<AudioSource>();
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
        enemyCheck = false;
        enemy2Check = false;

        isMoving = false;
        walkCheck = false;

        //audioSrc.Play();

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
        talkstop();

    }

    private void talkstop()
    {
        if(Inter.ck == true)
        {
            //isControl = false;          
        }
        if(Inter.ck != true)
        {
            //isControl = true;          // 이것때문에 isControl이 계속 true 고정됬었음
            Inter.Talkcount = 0; 
        }
        
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
            else if (c2d.gameObject.layer == 11)//enemy2
            {
                PlayerDamage(0);
                isGrounded = true;

                animator.SetTrigger("Idle");
                jumpCount = 2;
                break;
            }
            else if (c2d.gameObject.layer == 8)//enemy
            {
                PlayerDamage(0);

                isGrounded = true;
                animator.SetTrigger("Idle");
                jumpCount = 2;
                break;
            }
            else if(c2d.gameObject.layer == 13)
            {
                PlayerDamage(10);
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
        if(isGrounded == true && isGroundedPrev == false)
        {
            Debug.Log(jumpCount);
            animator.ResetTrigger("Jump_two");
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

    private void walkSound(bool isMoving)
    {
        
        if (isMoving == true && walkCheck == false)
        {
            audioSrc.Play();
            walkCheck = true;

        }
        else if(isMoving == false && walkCheck == true)
        {
            audioSrc.Stop();
            walkCheck = false;
        }
        
        
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
                walkSound(false);
                //SoundManager.instance.SFXPlayBool("Walk", walkClip, false);
                xAxis = 0;
                //audioSrc.Stop();
                animator.SetBool("IsWalk", false);
            }

            else
            {
                walkSound(true);
                //SoundManager.instance.SFXPlayBool("Walk", attackClip, true);

                //audioSrc.Play();

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
                    SoundManager.instance.SFXPlay("Attack", attackClip);
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
                        else if(c2d1.tag == "Enemy(x)")
                        {
                            if (criCheck == true)
                            {
                                criDmg = 2;
                                c2d1.GetComponent<EnemyMain2>().TakeDamage(Random.Range(200 * (int)criDmg, 299 * (int)criDmg));

                                continue;
                            }
                            c2d1.GetComponent<EnemyMain2>().TakeDamage(Random.Range(200, 299));
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
                        else if (c2d1.tag == "Boss")
                        {
                            if (criCheck == true)
                            {
                                criDmg = 2;
                                c2d1.GetComponent<BossMain>().TakeDamage(Random.Range(100 * (int)criDmg, 199 * (int)criDmg));
                                
                                continue;
                            }
                            c2d1.GetComponent<BossMain>().TakeDamage(Random.Range(100, 199));
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
            if (GateMain.nextL == 1)
            {
                transform.position = new Vector2(-15f, 145f);
            }
            transform.position = new Vector2(-20f, -8f);
            fade.black = true;
            hp = spawnhp;
        }
        else

        {
            float x = Mathf.Clamp(rb2d.velocity.x, -50, 50) ;
            float y = rb2d.velocity.y;

            rb2d.velocity = new Vector2(x, y);
            if(isControl == true)
            {
                if (transform.position.x > enemy2Tp.x)
                {
                    StartCoroutine(Hittingnomove());
                    rb2d.velocity = new Vector2(rb2d.velocity.x + 30, 0);
                    animator.SetTrigger("Hit");
                    hit = false;
                    //StartCoroutine(hittingnomove());
                }
                else if (transform.position.x < enemy2Tp.x)
                {
                    StartCoroutine(Hittingnomove());
                    rb2d.velocity = new Vector2(rb2d.velocity.x - 30, 0);
                    animator.SetTrigger("Hit");
                    hit = false;
                    //StartCoroutine(hittingnomove());
                }
            }
            //isControl = true;


        }
        
    }
    public void PlayerDamage2(int damage)
    {
        hp -= damage;
        hit = true;


        if (hp <= 0)
        {
            Debug.Log("Player Dead");
            if(GateMain.nextL == 1)
            {
                transform.position = new Vector2(-15f, 145f);
            }
            transform.position = new Vector2(-20f, -8f);
            fade.black = true;
            hp = spawnhp;
        }
        else

        {
            float x = Mathf.Clamp(rb2d.velocity.x, -50, 50);
            float y = rb2d.velocity.y;

            rb2d.velocity = new Vector2(x, y);
            if(isControl == true)
            {
                if (transform.position.x > enemyTp.x)
                {
                    StartCoroutine(Hittingnomove());
                    rb2d.velocity = new Vector2(rb2d.velocity.x + 30, 0);
                    animator.SetTrigger("Hit");
                    hit = false;
                    //StartCoroutine(hittingnomove());
                }
                else if (transform.position.x < enemyTp.x)
                {
                    StartCoroutine(Hittingnomove());
                    rb2d.velocity = new Vector2(rb2d.velocity.x - 30, 0);
                    animator.SetTrigger("Hit");
                    hit = false;
                    //StartCoroutine(hittingnomove());
                }
            }
            
            /*
            rb2d.velocity = new Vector2(rb2d.velocity.x - 20, 0);
            animator.SetTrigger("Hit");
            hit = false;
            StartCoroutine(Hittingnomove());
            */


        }

    }

    public void PlayerDamage3(int damage)
    {
        hp -= damage;
        hit = true;


        if (hp <= 0)
        {
            if (GateMain.nextL == 1)
            {
                transform.position = new Vector2(-15f, 145f);
            }
            transform.position = new Vector2(-20f, -8f);
            fade.black = true;
           // hp = 3;
        }
        else

        {
            float x = Mathf.Clamp(rb2d.velocity.x, -10, 10);
            float y = rb2d.velocity.y;

            rb2d.velocity = new Vector2(x, y);

            if (transform.position.x > BossMain.bossRb2d.transform.position.x)
            {
                //StartCoroutine(hittingnomove());
                rb2d.velocity = new Vector2(rb2d.velocity.x + 20, 0);
                animator.SetTrigger("Hit");
                hit = false;
                StartCoroutine(Hittingnomove());
            }
            else if (transform.position.x < BossMain.bossRb2d.transform.position.x)
            {
                //StartCoroutine(hittingnomove());
                rb2d.velocity = new Vector2(rb2d.velocity.x - 20, 0);
                animator.SetTrigger("Hit");
                hit = false;
                StartCoroutine(Hittingnomove());
            }


        }

    }


    public void enemy2Velocity(Vector3 tp)
    {
        enemy2Tp = tp;

    }

    public void enemyVelocity(Vector3 tp)
    {
        enemyTp = tp;

    }



    private void WallJumpCheck()
    {
        
        isWall = Physics2D.Raycast(WallCheck.position, Vector2.right * transform.localScale.x, wallCheckDistance, w_Layer);
        
    }

    private IEnumerator Hittingnomove()
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
                    SoundManager.instance.SFXPlay("Skill", skillClip);
                    BuffManager.instance.CreateBuff(type, per, duration, icon);
                    if(transform.localScale.x == -1)
                    {
                        animator.SetTrigger("Skill");
                        
                        Instantiate(bullet, pos.position, Quaternion.Euler(0, 180, 0));
                    }
                    else
                    {
                        animator.SetTrigger("Skill");
                        
                        Instantiate(bullet, pos.position, Quaternion.Euler(0, 0, 0));
                    }
                    
                    skillCurTime = skillCoolTime;
                }
                
            }
            else
            {
                //PlayerBullet.attackCheck = false;
                skillCurTime -= Time.deltaTime;
            }
            
        }
        else
        {
            return;
        }
        
    }


    
    
    







}
