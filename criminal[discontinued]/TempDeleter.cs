using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDeleter : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(Work());
    }
    public IEnumerator Work()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.parent != null && collision.transform.parent.tag == "Block")
        {
            Destroy(collision.transform.parent.gameObject);
        }
    }

}
