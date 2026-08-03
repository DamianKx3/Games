using System.Collections;
using UnityEngine;

public class LightPulse : MonoBehaviour
{
    Light Light;
    public float difference;
    public float speed;
    bool switch1;
    float maxval;
    bool flickeringnow;
    public int chanceOfFlicker;
    public bool reversefilcker;
    void Start()
    {
        if(Data.powerOut == true)
        {
            this.enabled = false;
            return;
        }
        Light = GetComponent<Light>();
        maxval = Light.intensity; // optymalnie speed = difference * 5
        StartCoroutine(flicker());
    }

    // Update is called once per frame
    void Update()
    {
        if(flickeringnow == true)
        {
            return;
        }
        if(switch1 == false)
        {
            Light.intensity = Light.intensity - speed * Time.deltaTime;
            if (maxval - difference > Light.intensity)
            {
                switch1 = true;
            }

        }
        else
        {
            Light.intensity = Light.intensity + speed * Time.deltaTime;
            if (maxval < Light.intensity)
            {
                switch1 = false;
            }
        }
    }
    public IEnumerator flicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if(Random.Range(0,100) < chanceOfFlicker)
            {
                flickeringnow = true;
                if(reversefilcker == true)
                {
                    Light.intensity = maxval * 10;
                    yield return new WaitForSeconds(0.1f);
                    Light.intensity = 0;
                }
                else
                {
                    Light.intensity = maxval / 10;
                    yield return new WaitForSeconds(0.1f);
                    Light.intensity = maxval / 1.5f;
                    yield return new WaitForSeconds(0.05f);
                    Light.intensity = maxval / 10;
                    yield return new WaitForSeconds(0.05f);
                    Light.intensity = maxval / 1.5f;
                }

                flickeringnow = false;
            }
        }
    }
}
