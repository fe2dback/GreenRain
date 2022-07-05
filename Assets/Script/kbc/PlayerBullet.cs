using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;
    public float skillDistance;
    public LayerMask isLayer;
    //public static bool attackCheck;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", 5);
        //attackCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, skillDistance, isLayer);
        if(ray.collider != null)
        {
            //attackCheck = false;
            if (ray.collider.tag == "Enemy")
            {
                ray.collider.GetComponent<EnemyMain>().TakeDamage(Random.Range(1000, 1100));
                Debug.Log("스킬타격");
            }
            else if (ray.collider.tag == "Enemy2")
            {
                ray.collider.GetComponent<Enemy2Main>().TakeDamage(Random.Range(500, 601));
                Debug.Log("스킬타격");
            }
            else if (ray.collider.tag == "Boss")
            {
                ray.collider.GetComponent<BossMain>().TakeDamage(Random.Range(500, 601));
                Debug.Log("스킬타격");
            }
            DestroyBullet();
        }
        if(transform.rotation.y == 0)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
            //transform.localScale = new Vector3(1, 1, 1);
            
        }
        else
        {
            
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
            //transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
