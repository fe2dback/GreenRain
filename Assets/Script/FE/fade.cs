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
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        stop();


    }
    private void FixedUpdate()
    {
        
    }


    private void stop()
    {
        if (black == true)
        {
            Debug.Log("ddd");
            animator.SetTrigger("blacka");
            black = false;
        }
    }

    
}

