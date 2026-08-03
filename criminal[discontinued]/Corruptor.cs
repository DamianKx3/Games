using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corruptor : MonoBehaviour
{
    bool lock1;
    public bool move;
    public Sprite[] sprites;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            lock1 = true;
            //StartCoroutine(corrupt());

        }
    }
    public IEnumerator corrupt()
    {
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < 10; i++)
        {
            mov();
        }
        while (true)
        {
            yield return null;
            mov();
        }
    }
    public void mov()
    {
        transform.position = transform.position + new Vector3(2, 0, 0);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.transform.parent != null && collision.transform.parent.tag == "Block")
        {
            if(sprites[collision.transform.parent.GetComponent<Blocks>().ID] != null)
            {
                collision.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[collision.transform.parent.GetComponent<Blocks>().ID];
            }
            
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
}
