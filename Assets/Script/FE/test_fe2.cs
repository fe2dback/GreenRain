using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_fe2 : MonoBehaviour
{
    SpriteRenderer render;
    public static float alp = 0;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        
    }
    private void FixedUpdate()
    {
        upp();
    }
  
    private void upp()
    {
        

        if (PlayerMain.interactionhas == true)
        {
            alp += Time.deltaTime;
            if(alp > 2f)
            {
                Debug.Log("hello"); //확인용
                PlayerMain.interactionhas = false;
                alp = 0;
                render.color = Color.red;// 확인용
            }
            
        }
    }

}
