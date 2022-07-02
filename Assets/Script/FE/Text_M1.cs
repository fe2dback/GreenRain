using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_M1 : MonoBehaviour
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
        
        if(Inter.check == true)
        {
            animator.SetTrigger("Talk_IDLE");
            Inter.check = false;
        }
    }

    private void talkani()
    {
        
        switch (Inter.Talkcount)
            {
                case 1:
                    animator.SetTrigger("Talk_1");
                    Inter.Talkcount++;

                    break;

                case 3:
                    animator.SetTrigger("Talk_IDLE");
                    animator.SetTrigger("Talk_2");
                    Inter.Talkcount++;
                    break;



                case 7:
                animator.SetTrigger("Talk_IDLE");
                animator.SetTrigger("Talk_4");
                    Inter.Talkcount++;
                    break;

                case 9:
                animator.SetTrigger("Talk_IDLE");
                animator.SetTrigger("Talk_5");
                Inter.Talkcount++;
                break;
     

        }
            
        
    }
}
