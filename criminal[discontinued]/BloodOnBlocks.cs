using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodOnBlocks : MonoBehaviour
{
    public GameObject bloodhard;
    public GameObject bloodmedium;
    public GameObject bloodeasy;
    public GameObject bloodlight;
    public GameObject bloodVerylight;
    bool lock1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x < Random.Range(4f,5f))
        {
            transform.localScale = transform.localScale + new Vector3(10, 10, 0) * Time.deltaTime;
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.parent && collision.transform.parent.tag == "Block")
        {
            if (collision.transform.parent.gameObject.GetComponent<Blocks>().CantleaveBlood == true)
            {
                return;
            }
            if(Random.Range(0,4) < 1)
            {
                return;
            }
            GameObject G;

            if (transform.localScale.x < 0.4f)
            {
                G = GameObject.Instantiate(bloodhard, transform.position, Quaternion.identity);


            }
            else if (transform.localScale.x < 1f)
            {
                G = GameObject.Instantiate(bloodmedium, transform.position, Quaternion.identity);

            }
            else if (transform.localScale.x < 2.5f)
            {
                G = GameObject.Instantiate(bloodeasy, transform.position, Quaternion.identity);
            }
            else if (transform.localScale.x < 4)
            {
                G = GameObject.Instantiate(bloodlight, transform.position, Quaternion.identity);

            }
            else
            {
                G = GameObject.Instantiate(bloodVerylight, transform.position, Quaternion.identity);


            }
            G.transform.parent = collision.transform;
            G.transform.localPosition = new Vector3(0, 0, -0.1f);
            G.transform.rotation = collision.transform.rotation;
            G.GetComponent<SpriteRenderer>().sortingOrder = collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
            int r = Random.Range(0, 4);
            if (r == 1)
            {
                G.GetComponent<SpriteRenderer>().flipX = true;
            }
            if (r == 2)
            {
                G.GetComponent<SpriteRenderer>().flipY = true;
            }
            if (r == 3)
            {
                G.GetComponent<SpriteRenderer>().flipX = true;
                G.GetComponent<SpriteRenderer>().flipY = true;
            }
        }
        
        
    }
}
