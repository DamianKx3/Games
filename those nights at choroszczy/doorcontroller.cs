using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorcontroller : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public AudioSource jp;

    public GameObject doorsleft;
    public GameObject doorsright;

    public GameObject doorbuttonleft;
    public GameObject doorbuttonright;

    public Animator leftanim;
    public Animator rightanim;

    public GameObject darknessleft;
    public GameObject darknessright;

    public Light lightleft;
    public Light lightright;

    public bool leftopen;
    public bool rightopen;

    public bool lla;
    public bool rla;
    bool done;
    public TimeAndPower TimeAndPower;
    public jumpscareController jumpscareController;
    public bool cooldown1;

    void Start()
    {
         leftopen = true;
         rightopen = true;
        lightleft.enabled = false;
        lightright.enabled = false;
        lla = true;
        rla = true;
        done = false;
        cooldown1 = true;

    }

    IEnumerator cd()
    {
        yield return new WaitForSeconds(0.1f);
        cooldown1 = true;
    }
    void Update()
    {
        if(TimeAndPower.power > 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (jumpscareController.caninteract == true)
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width /2, Screen.height /2, 0));
                    if (Physics.Raycast(ray, out hit))
                    {
                        if(cooldown1 == true)
                        {
                            cooldown1 = false;
                            StartCoroutine(cd());
                            // Debug.Log(hit.collider.name);
                            if (hit.collider.name == "doorbutton")
                            {
                                
                                if (leftopen == true)
                                {
                                    leftopen = false;
                                    TimeAndPower.usage++;
                                    leftanim.SetTrigger("close");
                                    audioSource.Play();

                                }
                                else
                                {
                                    leftopen = true;
                                    TimeAndPower.usage--;
                                    leftanim.SetTrigger("open");
                                    audioSource.Play();

                                }
                            }
                            if (hit.collider.name == "jp")
                            {

                                jp.pitch = Random.Range(0.9f, 1.1f);
                                jp.Play();
                            }

                            if (hit.collider.name == "doorbutton2")
                            {


                                if (rightopen == true)
                                {
                                    rightopen = false;
                                    TimeAndPower.usage++;
                                    rightanim.SetTrigger("close");
                                    audioSource.Play();
                                }
                                else
                                {


                                    rightopen = true;
                                    TimeAndPower.usage--;
                                    rightanim.SetTrigger("open");
                                    audioSource.Play();
                                }
                            }
                        }
                        

                        if (hit.collider.name == "lightbutton")
                        {
                            if (lla == true)
                            {
                                StartCoroutine(lightforleft());
                            }

                        }


                        if (hit.collider.name == "lightbutton2")
                        {
                            if (rla == true)
                            {
                                StartCoroutine(lightforright());
                            }
                        }
                    }
                }
               
               
            }
        }
        else
        {

            TimeAndPower.power = 0;
            if (done == false)
            {

                done = true;
                rightanim.SetTrigger("open");
                leftanim.SetTrigger("open");
                audioSource.Play();
                audioSource3.Play();
            }
            rightopen = false;
            leftopen = false;
           
        }
        
    }

    IEnumerator lightforleft()
    {
        TimeAndPower.usage++;
        audioSource2.Play();
        lla = false;
        darknessleft.SetActive(false);
        lightleft.enabled = true;
        yield return new WaitForSeconds(1f);
        lla = true;
        TimeAndPower.usage--;
        darknessleft.SetActive(true);
        lightleft.enabled = false;
    }
    IEnumerator lightforright()
    {
        rla = false;
        TimeAndPower.usage++;
        audioSource2.Play();
        darknessright.SetActive(false);
        lightright.enabled = true;
        yield return new WaitForSeconds(1f);
        lightright.enabled = false;
        TimeAndPower.usage--;
        rla = true;
        darknessright.SetActive(true);
    }
}
