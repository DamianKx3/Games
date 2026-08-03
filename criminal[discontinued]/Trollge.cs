using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Trollge : MonoBehaviour
{
    public Sprite[] eye;
    public Sprite[] trollge;
    public Image img;
    public Image imgE1;
    public Image imgE2;
    int currenteye;
    void Start()
    {
        StartCoroutine(troll());
    }


    void Update()
    {
        
    }
    public IEnumerator troll()
    {
        yield return null;
        for (int i = 0; i < trollge.Length; i++)
        {
            img.sprite= trollge[i];
            yield return new WaitForSeconds(0.1f);
        }
        while (true)
        {
            yield return null;
            imgE1.sprite = eye[currenteye];
            imgE2.sprite = eye[currenteye];

            yield return new WaitForSeconds(0.05f);
            currenteye++;
            img.gameObject.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-1f,1f));
            if(currenteye > eye.Length-1)
            {
                currenteye = 0;
            }
        }
    }
}
