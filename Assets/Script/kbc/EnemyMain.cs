using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        rb2d1 = GetComponent<Rigidbody2D>();
        animator = transform.Find("Sprite").GetComponent<Animator>();
        check = transform.Find("Check");
        pos = transform.Find("bullet");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        if(raycast.collider != null)
        {
            

            if(Vector2.Distance(transform.position, raycast.collider.transform.position) < atkDistance)
            {
                if(currentTime <= 0)
                {
                    GameObject bulletCopy = Instantiate(bullet, pos.position, transform.rotation);
                    animator.SetTrigger("Shoot");

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

    public void TakeDamage(int damage)
    {
        hp -= damage;
        //rb2d.velocity = Vector2.zero;
        if(hp <= 0)
        {
            Debug.Log("Monster Dead");
        }
        else
        {
            if(transform.position.x > PlayerMain.rb2d.transform.position.x)
            {
                rb2d1.velocity = new Vector2(rb2d1.velocity.x + 20, 0);
            }
            else
            {
                rb2d1.velocity = new Vector2(rb2d1.velocity.x - 20, 0);
            }
        }
        

    }
}
