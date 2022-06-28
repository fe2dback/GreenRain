using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateMain : MonoBehaviour
{
    public GameObject targetObj;
    public GameObject toObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            targetObj = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(TeleoportRoutine());
        }
    }
    IEnumerator TeleoportRoutine()
    {
        yield return null;
        targetObj.GetComponent<PlayerMain>().isControl = false;

        yield return new WaitForSeconds(1);
        targetObj.transform.position = toObj.transform.position;

        yield return new WaitForSeconds(1);
        targetObj.GetComponent<PlayerMain>().isControl = true;
    }
}
