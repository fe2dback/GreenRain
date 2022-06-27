using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BtnType : MonoBehaviour
{
    
    public BTNType currentType;
    public void OnBtnClick()
    {
        switch(currentType)
        {
            case BTNType.NewGame:
                SceneManager.LoadScene("SampleScene");
                break;

            case BTNType.Option:
                Debug.Log("설정창");
                break;

            case BTNType.Back:
                Debug.Log("되돌아가기");
                break;

            case BTNType.Quit:
                Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false;
                break;
        }
    }
}
