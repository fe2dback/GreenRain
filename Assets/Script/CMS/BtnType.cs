using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnType : MonoBehaviour
{
    public BTNType currentType;
    public void OnBtnClick()
    {
        switch(currentType)
        {
            case BTNType.NewGame:
                Debug.Log("게임시작");
                break;

            case BTNType.Option:
                Debug.Log("설정창");
                break;

            case BTNType.Back:
                Debug.Log("되돌아가기");
                break;

            case BTNType.Quit:
                Debug.Log("게임종료");
                break;
        }
    }
}
