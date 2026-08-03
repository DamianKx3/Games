using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kononowicz : MonoBehaviour
{
    public float anger;
    public GameObject[] konon = new GameObject[10];
    public int state;
    public Controllpanel Controllpanel;
    public jumpscareController jumpscareController;
    public float look;
    public camformove camformove;

    public GameObject doors1st1;
    public GameObject doors1st2;
    public GameObject doors2st1;
    public GameObject doors2st2;
    public GameObject glass1;
    public GameObject glass2;
    public GameObject blackscreen;
    public AudioSource audioSource;
    void Start()
    {

        look = 1000;
        state = 0;
        for (int i = 0; i < konon.Length; i++)
        {
            konon[i].SetActive(false);
            
        }
        StartCoroutine(startgame());
    }
    private void Update()
    {
        //Debug.Log(look);
        if(look > 1000)
        {
            look = 1000;
        }
    }

    IEnumerator startgame()
    {
        konon[state].SetActive(true);
        yield return new WaitForSeconds(10);


        look = 1000;
        camformove.StartCoroutine(camformove.move4());
        while (state == 0)
        {
            yield return new WaitForSeconds(0.1f);
            //Debug.Log("aha " + Random.Range(0, 100));
            if (Controllpanel.panelopen == false || (Controllpanel.currentcamera != 4 && Controllpanel.panelopen == true))
            {
                look = look - Random.Range(0.5f,2f) * anger;
                //Debug.Log("if");

            }
            else
            {
                look = look + 100;
                //Debug.Log("else");
            }

            if(look < 1)
            {
                state++;
               
            }
        }
        doors1st1.SetActive(false);
        doors1st2.SetActive(true);


        camformove.StartCoroutine(camformove.move4());
        konon[state].SetActive(true);
        konon[state - 1].SetActive(false);
        look = 1000;
        while (state == 1)
        {
            yield return new WaitForSeconds(0.1f);
            //Debug.Log("aha " + Random.Range(0, 100));
            if (Controllpanel.panelopen == false || (Controllpanel.currentcamera != 4 && Controllpanel.panelopen == true))
            {
                look = look - Random.Range(0.5f, 2f) * anger;

            }
            else
            {
                look = look + 100;
            }
            if (look < 1)
            {
                state++;
            }
        }

        camformove.StartCoroutine(camformove.move4());
        camformove.StartCoroutine(camformove.move2());
        konon[state].SetActive(true);
        konon[state - 1].SetActive(false);
        look = 1000;
        while (state == 2)
        {
            yield return new WaitForSeconds(0.1f);
            //Debug.Log("aha " + Random.Range(0, 100));
            if (Controllpanel.panelopen == false || (Controllpanel.currentcamera != 2 && Controllpanel.panelopen == true))
            {
                look = look - Random.Range(0.5f, 2f) * anger;

            }
            else
            {
                look = look + 100;
            }
            if (look < 1)
            {
                state++;
            }
        }
        camformove.StartCoroutine(camformove.move2());
        konon[state].SetActive(true);
        konon[state - 1].SetActive(false);
        look = 1000;
        while (state == 3)
        {
            yield return new WaitForSeconds(0.1f);
            //Debug.Log("aha " + Random.Range(0, 100));
            if (Controllpanel.panelopen == false || (Controllpanel.currentcamera != 2 && Controllpanel.panelopen == true))
            {
                look = look - Random.Range(0.5f, 2f) * anger;

            }
            else
            {
                look = look + 100;
            }
            if (look < 1)
            {
                state++;
            }
        }
        camformove.StartCoroutine(camformove.move2());
        konon[state].SetActive(true);
        konon[state - 1].SetActive(false);
        look = 1000;
        while (state == 4)
        {
            yield return new WaitForSeconds(0.1f);
            //Debug.Log("aha " + Random.Range(0, 100));
            if (Controllpanel.panelopen == false || (Controllpanel.currentcamera != 2 && Controllpanel.panelopen == true))
            {
                look = look - Random.Range(0.5f, 2f) * anger;

            }
            else
            {
                look = look + 100;
            }
            if (look < 1)
            {
                state++;
            }
        }
        camformove.StartCoroutine(camformove.move2());
        konon[state].SetActive(true);
        konon[state - 1].SetActive(false);
        look = 1000;
        while (state == 5)
        {
            yield return new WaitForSeconds(0.1f);
            //Debug.Log("aha " + Random.Range(0, 100));
            if (Controllpanel.panelopen == false || (Controllpanel.currentcamera != 1 && Controllpanel.panelopen == true))
            {
                look = look - Random.Range(0.5f, 2f) * anger;

            }
            else
            {
                look = look + 100;
            }
            if (look < 1)
            {
                state++;
            }
        }
        camformove.StartCoroutine(camformove.move2());
        camformove.StartCoroutine(camformove.move1());
        konon[state].SetActive(true);
        konon[state - 1].SetActive(false);
        look = 1000;
        while (state == 6)
        {
            yield return new WaitForSeconds(0.1f);
            //Debug.Log("aha " + Random.Range(0, 100));
            if (Controllpanel.panelopen == false || (Controllpanel.currentcamera != 1 && Controllpanel.panelopen == true))
            {
                look = look - Random.Range(0.5f, 2f) * anger;

            }
            else
            {
                look = look + 100;
            }
            if (look < 1)
            {
                state++;
            }
        }
        camformove.StartCoroutine(camformove.move1());
        konon[state].SetActive(true);
        konon[state - 1].SetActive(false);
        look = 1000;
        doors2st1.SetActive(false);
        doors2st2.SetActive(true);
        while (state == 7)
        {
            yield return new WaitForSeconds(0.1f);
            //Debug.Log("aha " + Random.Range(0, 100));
            if (Controllpanel.panelopen == false || (Controllpanel.currentcamera != 1 && Controllpanel.panelopen == true))
            {
                look = look - Random.Range(0.5f, 2f) * anger;
            }
            else
            {
                look = look + 100;
            }
            if (look < 1)
            {
                state++;
            }
        }
        camformove.StartCoroutine(camformove.move1());
        konon[state].SetActive(true);
        konon[state - 1].SetActive(false);
        look = 1000;
        
        yield return new WaitForSeconds(Random.Range(18f,25f));
        blackscreen.SetActive(true);
        konon[state - 1].SetActive(false);
        konon[state].SetActive(false);
        glass1.SetActive(false);
        glass2.SetActive(true);
        audioSource.Play();
        yield return new WaitForSeconds(0.5f);
        blackscreen.SetActive(false);
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        jumpscareController.enemyID = 0;
        jumpscareController.StartJS();
    }
}
