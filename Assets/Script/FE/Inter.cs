using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inter : MonoBehaviour
{
    public static SpriteRenderer render;
    public static float alp;
    private float alp2;
    public static bool check;
    private bool ck;
    public static int Talkcount = 0;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        alp = 0;
        alp2 = 0;
        check = false;
        ck = false;

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
                Debug.Log("hello"); //È®ÀÎ¿ë
                
                alp = 0;
                ck = true;
                PlayerMain.interactionhas = false;
                Talkcount++;
                alp2 = 0;

            }

        }
        if(ck == true)
        {
            alp2 += Time.deltaTime;
            if (alp2 > 20f)
            {
                check = true;
                ck = false;
                Talkcount = 0;
                alp2 = 0;
            }
        }
        Debug.Log(alp2);
        
        
         
        
    }

    

}
