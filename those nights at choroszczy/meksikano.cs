using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meksikano : MonoBehaviour
{
    public float anger;
    public GameObject[] meksyk = new GameObject[6];
    public int state;
    public doorcontroller doorcontroller;

    public jumpscareController jumpscareController;
    public camformove camformove;
    void Start()
    {
        state = 0;
        for (int i = 0; i < meksyk.Length; i++)
        {
            meksyk[i].SetActive(false);

        }
        StartCoroutine(startgame());
    }

    
    void Update()
    {
        
    }
    IEnumerator startgame()
    {
        meksyk[state].SetActive(true);
        yield return new WaitForSeconds(10);

        while (state != 7)
        {
            state = 0;
            camformove.StartCoroutine(camformove.move3());
            for (int i = 0; i < meksyk.Length; i++)
            {
                meksyk[i].SetActive(false);

            }
            meksyk[state].SetActive(true);
            while (state == 0)
            {

                
                yield return new WaitForSeconds(1);
                if (Random.Range(0, 100) < 1 * anger)
                {
                    state++;
                }
            }
            camformove.StartCoroutine(camformove.move3());

            meksyk[state].SetActive(true);
            meksyk[state - 1].SetActive(false);

            while (state == 1)
            {
                yield return new WaitForSeconds(1);
                if (Random.Range(0, 100) < 1 * anger)
                {
                    state++;
                }
            }
            camformove.StartCoroutine(camformove.move3());
            camformove.StartCoroutine(camformove.move2());
            meksyk[state].SetActive(true);
            meksyk[state - 1].SetActive(false);

            while (state == 2)
            {
                yield return new WaitForSeconds(1);
                if (Random.Range(0, 100) < 1 * anger)
                {
                    state++;
                }
            }
            camformove.StartCoroutine(camformove.move2());
            camformove.StartCoroutine(camformove.move1());
            meksyk[state].SetActive(true);
            meksyk[state - 1].SetActive(false);

            while (state == 3)
            {
                yield return new WaitForSeconds(1);
                if (Random.Range(0, 100) < 1 * anger)
                {
                    state++;
                }
            }
            camformove.StartCoroutine(camformove.move1());
            meksyk[state].SetActive(true);
            meksyk[state - 1].SetActive(false);

            while (state == 4)
            {
                yield return new WaitForSeconds(1);
                if (Random.Range(0, 100) < 1 * anger)
                {
                    state++;
                }
            }
            camformove.StartCoroutine(camformove.move1());
            meksyk[state].SetActive(true);
            meksyk[state - 1].SetActive(false);

            

            yield return new WaitForSeconds(2);
            for (int i = 0; i < 16; i++)
            {
                yield return new WaitForSeconds(1);
                if (doorcontroller.leftopen == false && i > 10)
                {
                    state = 0;
                    i = 20;
                    
                }
            }
            if (state != 0)
            {
                state = 7;
            }
        }
        //smierc
        //Debug.Log("ded");
        jumpscareController.enemyID = 2;
        jumpscareController.StartJS();



    }
}
