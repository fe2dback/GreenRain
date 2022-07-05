using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T1 : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Inter.Talkcount =0;
        animator = transform.Find("Sprite").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        talk();
    }

    private void talk()

    {
        //Debug.Log(Inter.Talkcount);
        switch(Inter.Talkcount)
        {

            case 1:
                animator.SetTrigger("Ta1");
                Inter.Talkcount++;
                break;
            
            case 3:
                animator.SetTrigger("Ta2");
                Inter.Talkcount++;
                break;

            case 6:
                animator.SetTrigger("Idle");
                break;
                
            case 7:
                animator.SetTrigger("Ta4");
                Inter.Talkcount++;
                break;
            case 9:
                animator.SetTrigger("Ta5");
                Inter.Talkcount++;
                break;
            case 12:
                animator.SetTrigger("Idle2");
                break;


        }
            
        
    }
}
