using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;
    private Transform pos;
    public bool checkLs;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", 2);
        pos = GetComponent<Transform>();
        checkLs = false;
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerMain.hit = false;
        
        //RaycastHit2D raycast2 = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        
        if (EnemyMain2.Rs == true)
        {
            RaycastHit2D raycast2 = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
            if (raycast2.collider != null)
            {
                if (raycast2.collider.tag == "Player")
                {
                    //PlayerMain.enemyCheck = true;
                    raycast2.collider.GetComponent<PlayerMain>().enemyVelocity(transform.position);
                    raycast2.collider.GetComponent<PlayerMain>().PlayerDamage2(1);
                    Debug.Log("피격");

                }
                //PlayerMain.enemyCheck = false;
                EnemyMain2.Rs = false;
                DestroyBullet();
            }

            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
            if (raycast.collider != null)
            {
                if (raycast.collider.tag == "Player")
                {
                    //PlayerMain.enemyCheck = true;
                    raycast.collider.GetComponent<PlayerMain>().enemyVelocity(transform.position);
                    raycast.collider.GetComponent<PlayerMain>().PlayerDamage2(1);
                    Debug.Log("피격");

                }
                //PlayerMain.enemyCheck = false;
                DestroyBullet();
            }

            transform.Translate(transform.right * -1f * speed * Time.deltaTime);
        }

        //RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        
        
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public void localScaleCheck(bool check)
    {
        checkLs = check;
    }
}
