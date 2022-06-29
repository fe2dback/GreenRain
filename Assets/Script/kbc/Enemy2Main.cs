using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Main : MonoBehaviour
{
    private Rigidbody2D rb2d1;
    private Animator animator;
    private Transform check;
    public int hp;

    public float distance;
    public LayerMask isLayer;

    public float speed;
    public float atkDistance;

    public Transform pos;
    public Vector3 StartPosition;

    public float coolTime;
    private float currentTime;

    public float maxTime;
    public float curTime;

    public float timePassed;
    private bool isControl;


    // Start is called before the first frame update
    void Start()
    {
        rb2d1 = GetComponent<Rigidbody2D>();
        animator = transform.Find("Sprite").GetComponent<Animator>();
        check = transform.Find("Check");
        //StartPosition = transform.position;

        isControl = true;
    }

    private void FixedUpdate()
    {
        Enemy2Move();
        deadTime();
    }

    private void Enemy2Move()
    {
        if(isControl == true)
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
            if (raycast.collider != null)
            {
                curTime = 0;

                if (Vector2.Distance(transform.position, raycast.collider.transform.position) < atkDistance)
                {

                    if (currentTime <= 0)
                    {
                        if (raycast.collider.tag == "Player")
                        {
                            raycast.collider.GetComponent<PlayerMain>().PlayerDamage(1);
                            animator.SetTrigger("Attack");
                            Debug.Log("ÇÇ°Ý");
                        }

                        currentTime = coolTime;
                    }
                }
                else if (Vector2.Distance(transform.position, raycast.collider.transform.position) > distance)
                {
                    animator.SetBool("IsWalk", false);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, raycast.collider.transform.position, Time.deltaTime * speed);
                    transform.localScale = new Vector3(1, 1, 1);
                    animator.SetBool("IsWalk", true);
                }

                currentTime -= Time.deltaTime;

            }
            else
            {
                curTime += Time.deltaTime;
                if (curTime >= maxTime)
                {
                    if (transform.position != StartPosition)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, StartPosition, Time.deltaTime * speed);
                        transform.localScale = new Vector3(-1, 1, 1);
                        animator.SetBool("IsWalk", true);
                    }
                    else
                    {
                        animator.SetBool("IsWalk", false);
                        transform.localScale = new Vector3(1, 1, 1);
                    }

                }

            }
        }
        
    }




    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        
        hp -= damage;
        //rb2d.velocity = Vector2.zero;
        if (hp <= 0)
        {
            isControl = false;
            Debug.Log("Monster Dead");
            animator.SetTrigger("Die");
            //Destroy(gameObject);
           
        }
        else
        {
            if(isControl == true)
            {
                if (transform.position.x > PlayerMain.rb2d.transform.position.x)
                {
                    rb2d1.velocity = new Vector2(rb2d1.velocity.x + 20, 0);
                    animator.SetTrigger("Hit");
                }
                else
                {
                    rb2d1.velocity = new Vector2(rb2d1.velocity.x - 20, 0);
                    animator.SetTrigger("Hit");
                }
            }
        }


    }

    private void deadTime()
    {
        if(isControl == false)
        {
            timePassed += Time.deltaTime;
            if (timePassed > 1)
            {
                Destroy(gameObject);
            }
        }
    
    }


}
