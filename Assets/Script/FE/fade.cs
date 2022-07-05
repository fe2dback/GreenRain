using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour
{
    private Animator animator;
    public static bool black = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.Find("Square").GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (black == true)
        {
            animator.SetTrigger("blacka");
            black = false;
        }



    }
    private void FixedUpdate()
    {
        
    }

    
}

