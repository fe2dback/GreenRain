using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2 : MonoBehaviour
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

        switch (Inter.Talkcount)
        {
            
            case 5:
                animator.SetTrigger("Ta3");
                Inter.Talkcount++;
                break;
            case 7:
                animator.SetTrigger("Idle");
                break;
            case 11:
                animator.SetTrigger("Ta6");
                Inter.Talkcount++;
                if(Inter.alp2 > 5f)
                {
                    Inter.Talkcount++;
                }
                break;
                


        }


    }
}
