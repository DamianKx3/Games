using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class blackoutFinal : MonoBehaviour
{
    public Image img;
    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(0,0,0,1);
        img.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator dupa()
    {
        img.enabled = true;
        yield return new WaitForSeconds(0.2f);
        img.enabled = false;
    }
}
