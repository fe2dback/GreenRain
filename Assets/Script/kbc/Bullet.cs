using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;
    private Transform pos;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", 2);
        pos = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerMain.hit = false;
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        if (raycast.collider != null)
        {
            if(raycast.collider.tag == "Player")
            {
                raycast.collider.GetComponent<PlayerMain>().PlayerDamage(1);
                Debug.Log("ÇÇ°Ý");
           
            }
            DestroyBullet();
        }

        transform.Translate(transform.right * -1f * speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
