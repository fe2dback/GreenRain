using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    public static bool black;

    public Transform target;
    private Vector3 targetPos;
    public float speed;

    public Vector2 center;
    public Vector2 size;

    private float height;
    private float widht;
    

    // Start is called before the first frame update
    void Start()
    {
        
        height = Camera.main.orthographicSize;
        widht = height * Screen.width / Screen.height;
        targetPos = target.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    void Update()
    {
        targetPos = new Vector3(target.position.x + 5, target.position.y + 7, target.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {

        //transform.position = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Lerp(transform.position, targetPos /* 1.15f*/, Time.deltaTime * speed);
        //transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        float x = size.x * 0.5f - widht;
        float clampX = Mathf.Clamp(transform.position.x, -x + center.x, x + center.x);

        float y = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -y + center.y, y + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
