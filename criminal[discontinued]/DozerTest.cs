using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DozerTest : MonoBehaviour
{
    public bool OnTrigger;
    public GameObject blockNow;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.transform.parent != null && collision.isTrigger == false && collision.transform.parent.tag == "Block")
        {
            blockNow = collision.transform.parent.gameObject;
            OnTrigger = true;
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform.parent != null && collision.isTrigger == false && collision.transform.parent.tag == "Block")
        {
            OnTrigger = false;
        }
    }
}
