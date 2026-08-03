using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LenghtCheck : MonoBehaviour
{
    public bool move;
    public GameObject ground;
    void Start()
    {
        StartCoroutine(Border());
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > 25)
        {
            Camera.main.GetComponent<CameraController>().LeftLimit = transform.position.x;
        }
        if(move == true)
        {
            ground.SetActive(false);
        }
        else
        {
            ground.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.transform.parent != null && collision.transform.parent.tag == "Block")
        {
            move = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
 

        if (collision.transform.parent != null && collision.transform.parent.tag == "Block")
        {
            move = true;
        }
    }
    public IEnumerator Border()
    {
        yield return new WaitForSeconds(0.05f);
        while(true)
        {
            yield return null;
            if (transform.position.x > 25)
            {
                if (move == true)
                {
                    transform.position = transform.position + new Vector3(100 * Time.deltaTime, 0, 0);

                }
            }
            else
            {
                transform.position = transform.position + new Vector3(50 * Time.deltaTime, 0, 0);
            }


        }

    }
}
