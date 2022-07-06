using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain2 : MonoBehaviour
{
    private Rigidbody2D rb2d1;
    private Animator animator;
    private Transform check;
    public int hp;


    public float distance;
    public LayerMask isLayer;

    public float speed;
    public float atkDistance;

    public GameObject bullet;
    Transform pos;

    public float coolTime;
    private float currentTime;

    public float timePassed;
    private bool isControl;

    public GameObject hudDamageText;
    public Transform hudPos;

    public static bool Rs;

    // Start is called before the first frame update
    void Start()
    {
        rb2d1 = GetComponent<Rigidbody2D>();
        animator = transform.Find("Sprite").GetComponent<Animator>();
        check = transform.Find("Check");
        pos = transform.Find("bullet");
        isControl = true;
        Rs = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        EnemyMove();
        deadTime();

    }



    private void EnemyMove()
    {
        if (isControl == true)
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
            if (raycast.collider != null)
            {


                if (Vector2.Distance(transform.position, raycast.collider.transform.position) < atkDistance)
                {
                    if (currentTime <= 0)
                    {
                        //bullet.GetComponent<Bullet>().localScaleCheck(true);
                        Rs = true;
                        animator.SetTrigger("Shoot");
                        GameObject bulletCopy = Instantiate(bullet, pos.position, Quaternion.Euler(0, 180, 0));
                        
                        
                        //GameObject bulletCopy = Instantiate(bullet, pos.position, transform.rotation);
                        //animator.SetTrigger("Shoot");

                        currentTime = coolTime;
                    }
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, raycast.collider.transform.position, Time.deltaTime * speed);
                }

                currentTime -= Time.deltaTime;

            }
        }
    }

    public void TakeDamage(int damage)
    {
        GameObject hudText = Instantiate(hudDamageText);
        hudText.transform.position = hudPos.position;
        hudText.GetComponent<DamageText>().damage = damage;

        hp -= damage;
        //rb2d.velocity = Vector2.zero;
        if (hp <= 0)
        {
            isControl = false;
            Debug.Log("Monster Dead");
            animator.SetTrigger("Dead");
        }
        else
        {
            if (isControl == true)
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
        if (isControl == false)
        {
            timePassed += Time.deltaTime;
            if (timePassed > 1)
            {
                Destroy(gameObject);
            }
        }

    }
}
