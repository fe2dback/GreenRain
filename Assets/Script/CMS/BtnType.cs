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
                Debug.Log("���ӽ���");
                break;

            case BTNType.Option:
                Debug.Log("����â");
                break;

            case BTNType.Back:
                Debug.Log("�ǵ��ư���");
                break;

            case BTNType.Quit:
                Debug.Log("��������");
                break;
        }
    }
}
