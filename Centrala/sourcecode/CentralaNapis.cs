using UnityEngine;
using System.Collections;
public class CentralaNapis : MonoBehaviour
{
    public Material NapisMain;
    public Material L;
    bool flickeringnow;
    public int chanceOfFlicker;
    public float difference;
    public float speed;
    float maxval;
    float maxvalL;
    bool switch1;
    float a;
    float skipframes;
    void Start()
    {
        if (Data.powerOut == true)
        {
            L.SetColor("_EmissionColor", new Color(0, 0, 0, 1));
            NapisMain.SetColor("_EmissionColor", new Color(0, 0, 0, 1));

            this.enabled = false;
            return;
        }
        L.SetColor("_EmissionColor", new Color(2.7f, 2.7f, 2.7f, 1));
        NapisMain.SetColor("_EmissionColor", new Color(1.3f, 1.3f, 1.3f, 1));
        StartCoroutine(flicker());
        //maxval = NapisMain.GetColor("_emmisionColor");//3.4 -3.9
        maxval = 2.5f;
        a = maxval;
    }

    // Update is called once per frame
    void Update()
    {

        if (switch1 == false)
        {
            a = a - speed * Time.deltaTime;
            if (maxval - difference > a)
            {
                switch1 = true;
            }

        }
        else
        {
            a = a + speed * Time.deltaTime;
            if (maxval < a)
            {
                switch1 = false;
            }
        }
        if (skipframes < 0.05f)
        {
            skipframes = skipframes + Time.deltaTime;
            return;
        }
        else
        {
            skipframes = 0;
        }
        NapisMain.SetColor("_EmissionColor",new Color(a * a,0,0,1));
        if(flickeringnow == false)
        {
            L.SetColor("_EmissionColor", new Color((a - 1) * (a- 1), 0, 0, 1));

        }
    }
    public IEnumerator flicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if (Random.Range(0, 100) < chanceOfFlicker)
            {
                flickeringnow = true;
                L.SetColor("_EmissionColor", new Color(0, 0, 0, 1));
                yield return new WaitForSeconds(0.1f);
                L.SetColor("_EmissionColor", new Color((a - 1) * (a - 1), 0, 0, 1));
                yield return new WaitForSeconds(0.1f);
                L.SetColor("_EmissionColor", new Color(0, 0, 0, 1));
                yield return new WaitForSeconds(0.1f);
                flickeringnow = false;
            }
        }
    }

}
