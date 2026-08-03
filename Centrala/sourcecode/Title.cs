using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Title : MonoBehaviour
{
    public Image img;
    public TextMeshProUGUI title;
    float a;
    bool l;
    void Start()
    {
        a = 1;
    }

    // Update is called once per frame
    void Update()
    {
        img.color = new Color(0,0,0,a);
        if(a > 0)
        {
            a = a - Time.deltaTime;
        }
        else
        {
            a = 0;
            if(l == false)
            {
                l = true;
                StartCoroutine(flash());
            }
        }

    }
    IEnumerator flash()
    {
        yield return new WaitForSeconds(0.6f);
        title.gameObject.SetActive(false);        
        yield return new WaitForSeconds(0.05f);
        title.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        title.gameObject.SetActive(false);
    }
}
