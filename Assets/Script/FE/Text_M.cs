using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_M : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    private void FixedUpdate()
    {
        talkani();
        
    }

    private void talkani()
    {
        
            switch (Inter.Talkcount)
            {
                case 5:
                animator.SetTrigger("Talk_IDLE");
                animator.SetTrigger("Talk_3");
                Inter.Talkcount++;

                break;


                case 11:
                animator.SetTrigger("Talk_IDLE");
                animator.SetTrigger("Talk_6");
                    Inter.Talkcount++;

                    Inter.Talkcount = 0;
                    

                    break;

        }
        
    }
}
