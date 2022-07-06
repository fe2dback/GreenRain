using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class realchat : MonoBehaviour
{
    private Animator animator;
    int i = 0;
    float j = 0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.Find("to").GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("go");
            i++;
        }
        if(i > 5)
        {
            j += Time.deltaTime;
            animator.SetBool("gogo", true);
            
            if (j > 3.5f)
            {
                SceneManager.LoadScene("TutorialScene");
                animator.SetBool("gogo", false);
                j = 0;
                i = 0;
            }
        }
        
    }
}
