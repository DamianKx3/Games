using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGround : MonoBehaviour
{
    public bool OnG;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if(collision.transform.parent && collision.transform.parent.tag == "Block")
        {
            OnG = true;
        }
    }
}
