using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class major : MonoBehaviour
{
    public float anger;
    public GameObject[] majorsuch = new GameObject[4];
    public int state;
    public bool gas;
    public Controllpanel Controllpanel;
    public jumpscareController jumpscareController;
    public camformove camformove;

    public GameObject crate1;
    public GameObject crate2;
    void Start()
    {
        state = 0;
        for (int i = 0; i < majorsuch.Length; i++)
        {
            majorsuch[i].SetActive(false);

        }
        StartCoroutine(startgame());
    }


    void Update()
    {
        
    }
    IEnumerator startgame()
    {
        camformove.StartCoroutine(camformove.move6());
        majorsuch[state].SetActive(true);
        yield return new WaitForSeconds(10);
        
        while (state != 4)
        {
            camformove.StartCoroutine(camformove.move6());
            state = 0;
            crate1.SetActive(true);
            crate2.SetActive(false);
            while (state == 0)
            {

                for (int i = 0; i < majorsuch.Length; i++)
                {
                    majorsuch[i].SetActive(false);

                }
                majorsuch[state].SetActive(true);
                yield return new WaitForSeconds(1);
                if (Random.Range(0, 100) < 1 * anger)
                {
                    state++;
                }
            }
            camformove.StartCoroutine(camformove.move6());
            majorsuch[state].SetActive(true);

            majorsuch[state - 1].SetActive(false);

            while (state == 1)
            {
                yield return new WaitForSeconds(1);
                if (Random.Range(0, 100) < 1 * anger)
                {
                    state++;
                }
            }
            camformove.StartCoroutine(camformove.move6());
            majorsuch[state].SetActive(true);
            majorsuch[state - 1].SetActive(false);

            while (state == 2)
            {
                yield return new WaitForSeconds(1);
                if (Random.Range(0, 100) < 1 * anger)
                {
                    state++;
                }
            }
            camformove.StartCoroutine(camformove.move6());
            majorsuch[state].SetActive(true);
            majorsuch[state - 1].SetActive(false);
            crate1.SetActive(false);
            crate2.SetActive(true);
            while (state == 3)
            {
                yield return new WaitForSeconds(1);
                if (Random.Range(0, 100) < 1 * anger)
                {
                    state++;
                }
            }
            camformove.StartCoroutine(camformove.move6());
            for (int i = 0; i < majorsuch.Length; i++)
            {
                majorsuch[i].SetActive(false);

            }
            for (int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(1);
                if(gas == true)
                {
                    state = 0;
                    i = 10;
                    yield return new WaitForSeconds(2);
                }
            }
        }
        yield return new WaitForSeconds(2);
        jumpscareController.enemyID = 1;
        jumpscareController.StartJS();
        //smierc




    }
}