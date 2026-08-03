using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TriggerHuman : MonoBehaviour
{
    public bool OnTrigger;
    public bool follow;
    public GameObject node;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.transform.parent != null && collision.isTrigger == false && collision.transform.parent.tag == "Block" || collision.transform.tag == "ground")
        {
            OnTrigger = true;
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform.parent != null && collision.isTrigger == false && collision.transform.parent.tag == "Block" || collision.transform.tag == "ground")
        {
            OnTrigger = false;
        }
    }
    private void Update()
    {
        if(follow == true)
        {
            transform.position = node.transform.position;
            transform.rotation = node.transform.rotation;
            
        }
    }
}
