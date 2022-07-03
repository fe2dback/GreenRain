using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager instance;
    public GameObject buffPrefab;

    private void Awake()
    {
        instance = this;
    }

    public void CreateBuff(string type, float per, float du, Sprite icon)
    {
        GameObject go = Instantiate(buffPrefab, transform);
        go.GetComponent<BaseSkill>().Init(type, per, du);
        go.GetComponent<UnityEngine.UI.Image>().sprite = icon;
    }
    
}
