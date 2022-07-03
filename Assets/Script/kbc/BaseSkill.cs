using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSkill : MonoBehaviour
{
    public string type;
    public float percentage;
    public float duration;
    public float currentTime;
    public Image icon;

    WaitForSeconds seconds = new WaitForSeconds(0.1f);

    private void Awake()
    {
        icon = GetComponent<Image>();
    }

    public void Init(string type, float per, float du)
    {
        this.type = type;
        percentage = per;
        duration = du;
        currentTime = duration;
        icon.fillAmount = 1;
        Execute();
    }

    public void Execute()
    {

        StartCoroutine(Activation());
    }

    IEnumerator Activation()
    {
        while(currentTime > 0)
        {
            currentTime -= 0.1f;
            icon.fillAmount = currentTime / duration;
            yield return seconds;
        }
        icon.fillAmount = 0;
        currentTime = 0;

        DeActivation();
    }

    public void DeActivation()
    {
        Destroy(gameObject);
    }
}
