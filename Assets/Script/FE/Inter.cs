using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inter : MonoBehaviour
{
    public static SpriteRenderer render;
    private Animator animator;
    public static float alp;
    public static float alp2;
    public static bool check;
    public static bool ck;
    public static int Talkcount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if(Talkcount >= 12)
        {
            ck = false;
        }

        

        if (PlayerMain.interactionhas == true)
        {

            alp += Time.deltaTime;
            fade.black = true;
            
            if(alp > 1f)
            {

                SceneManager.LoadScene("realityroom");
                Debug.Log("hello"); //Ȯ�ο�
                
                alp = 0;
                ck = true;
                PlayerMain.interactionhas = false;
                Talkcount++;
                alp2 = 0;
                

            }

        }
      
    }

    

}
