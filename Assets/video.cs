using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class video : MonoBehaviour
{
    public VideoPlayer player;
    
    private void Start()
    {
        player = GetComponent<VideoPlayer>();
        
        
    }
    // Start is called before the first frame update
    private void Update()
    {
        if(player.isPaused)
        {
            SceneManager.LoadScene("TutorialScene");
        }
        
        
        
        
    }
}
