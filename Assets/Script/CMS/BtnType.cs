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
            case BTNType.GameStart:
                SceneManager.LoadScene("Video");
                break;

            case BTNType.NewGame:
                Debug.Log("�Ѿ��");
                SceneManager.LoadScene("TutorialScene");
                break;

            case BTNType.Option:
                Debug.Log("����â");
                break;

            case BTNType.Back:
                Debug.Log("�ǵ��ư���");
                break;

            case BTNType.Quit:
                Application.Quit();
                //UnityEditor.EditorApplication.isPlaying = false;
                break;
        }
    }
}
