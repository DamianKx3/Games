using System.Collections;
using UnityEngine;

public class shadowcontroller : MonoBehaviour
{
    public GameObject[] shadows;
    public Controllpanel Controllpanel;
    public int currentshadow;
    public bool canPlay;
    public AudioSource AudioSource;
    void Start()
    {
        currentshadow = -2;

        StartCoroutine(enumerator2());
    }

 
    void Update()
    {
        if (canPlay == true && AudioSource.isPlaying == false)
        {
            AudioSource.Play();
        }
        if(canPlay == false)
        {
            AudioSource.Stop();
        }

    }
    IEnumerator enumerator2()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10, 20));
            while (Controllpanel.panelopen == true)
            {
                yield return null;
            }
            StartCoroutine(enumerator());



        }
    }
    IEnumerator enumerator()
    {

        yield return null;

        if (currentshadow > -1)
        {
             shadows[currentshadow].SetActive(false);
        }

        currentshadow = Random.Range(0, 8);
        while (currentshadow == 6)
        {
            currentshadow = Random.Range(0, 8);
            yield return null;
        }
        shadows[currentshadow].SetActive(true);
        int tempshadow = currentshadow;
        while (currentshadow != Controllpanel.currentcamera)
        {
            yield return null;
            if (currentshadow == Controllpanel.currentcamera)
            {
                StartCoroutine(destr());

            }
            if (tempshadow != currentshadow)
            {

                break;
            }
        }



        


    }
    IEnumerator destr()
    {
        yield return null;
        Vector3 pos = gameObject.transform.position;
        bool temp2 = false;
        canPlay = true;
        AudioSource.pitch = 1;
        for (float i = 1; i < 40; i++)
        {
            AudioSource.pitch = 1f + i / 50;
            Debug.Log(1 + i / 100);
            yield return new WaitForSeconds(0.2f / i);
            gameObject.transform.position = gameObject.transform.position + new Vector3(0.1f, 0, 0);
            yield return new WaitForSeconds(0.2f / i);
            gameObject.transform.position = gameObject.transform.position + new Vector3(-0.1f, 0, 0);
            if (currentshadow != Controllpanel.currentcamera)
            {
                StartCoroutine(enumerator());
                temp2 = true;
                canPlay = false;
                break;

            }
        }
        if (temp2 == false)
        {
            gameObject.transform.position = pos;
            if (currentshadow == Controllpanel.currentcamera)
            {
                Controllpanel.laptopbatery = 0;
            }
            canPlay = false;
            StartCoroutine(enumerator());
        }
        

    }
}
