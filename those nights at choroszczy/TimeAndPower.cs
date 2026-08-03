using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TimeAndPower : MonoBehaviour
{
    public int AM;
    public float power;
    public int usage;

    public Text timetext;
    public Text procent;
    public Slider usageUI;
    public Slider powerUI;
    public bool virus;

    public Transform fekalia;
    public float anger;

    public bool isimpossible;
    public int sec;
    void Start()
    {
        virus = false;
        power = 100;
        usage = 1;
        AM = 0;
        sec = 0;
        StartCoroutine(time());
    }

    
    void Update()
    {
        if(AM == 2137)
        {
            SceneManager.LoadScene(3);
        }
        fekalia.localScale = new Vector3(AM, AM, 1);
       if(virus == false)
       {
            procent.text = "energia: " + Mathf.Round(power) + "%";
            usageUI.value = usage;
            powerUI.value = power;
            if(isimpossible == false)
            {
                if (AM != 0)
                {
                    timetext.text = AM + " AM";
                }
                else
                {
                    timetext.text = "12 AM";
                }
            }
            else
            {
                timetext.text = sec + " sec.";
            }


          
       }
       else
       {
            timetext.text = Random.Range(0, 1238) + "AM";
            procent.text = "energia: " + Random.Range(0, 1238) + "%";
            usageUI.value = Random.Range(0, 10);
            powerUI.value = Random.Range(0, 100);
       } 
       
        power = power - 0.2f * (usage -anger / 2) * Time.deltaTime; 
        if(power <= 0)
        {
            power = 0;
            //koniec energii
        }
    }
    IEnumerator time()
    {
        if(isimpossible == false)
        {
            while (AM != 6)
            {
                yield return new WaitForSeconds(60f);
                AM++;
            }
            SceneManager.LoadScene(3);
            //koniec nocy
            yield return null;
        }
        else
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                sec++;
                if (PlayerPrefs.GetInt("sec1") < sec)
                {
                    PlayerPrefs.SetInt("sec1", sec);
                }

            }

        }

    }
}
