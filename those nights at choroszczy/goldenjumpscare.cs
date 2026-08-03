using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldenjumpscare : MonoBehaviour
{
    public GameObject game1;
    public GameObject game2;
    void Start()
    {

        StartCoroutine(enumerator());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator enumerator()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            game1.SetActive(false);
            game2.SetActive(true);
            yield return new WaitForSeconds(0.05f);
            game1.SetActive(true);
            game2.SetActive(false);
        }


    }
}
