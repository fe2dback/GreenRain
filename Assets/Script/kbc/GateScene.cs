using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateScene : MonoBehaviour
{
    public GameObject targetObj;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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

        yield return new WaitForFixedUpdate();
        SceneManager.LoadScene("SampleScene");
        //SoundManager.instance.MusicStop(true);

        yield return new WaitForSeconds(0.7f);
        targetObj.GetComponent<PlayerMain>().isControl = true;
    }
}
