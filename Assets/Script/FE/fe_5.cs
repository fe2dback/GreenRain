using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class fe_5 : MonoBehaviour

{
    Sprite data;
    Transform size;
    private IEnumerator Move(Transform i)

    {



        for (; ; )

        {
            int rs = Random.Range(5, 30);
            int rd = Random.Range(-50, 450);
            i.transform.position -= new Vector3(0, rs, 0) ;

            if(i.transform.position.y < -15f)
            {
                i.transform.position = new Vector3(rd, 330f, 0);
            }
            

            yield return new WaitForFixedUpdate();

        }

    }

    // Start is called before the first frame update

    void Start()

    {

        data = GameObject.Find("rain 3").GetComponent<SpriteRenderer>().sprite;
        size = GetComponent<Transform>();




        GameObject[] blocks;

        blocks = new GameObject[400];

        for (int i = 0; i < 400; i++)

        {

            blocks[i] = new GameObject();

            blocks[i].transform.parent = this.transform;

            blocks[i].AddComponent<SpriteRenderer>();

            blocks[i].GetComponent<SpriteRenderer>().sprite = data;

            blocks[i].name = i.ToString();

            //blocks[i].transform.localScale = new Vector3(0.5f, 1f, 0);

            blocks[i].transform.position = new Vector3(i, 0, 0);



            StartCoroutine(Move(blocks[i].transform));



        }

    }



    // Update is called once per frame

    void Update()

    {



    }

}