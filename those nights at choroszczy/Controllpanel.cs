using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Controllpanel : MonoBehaviour
{
    public Material[] cameras;
    public Material butonsel;
    public Material butonnorm;
    public int currentcamera = 0;
    public GameObject screen;
    public GameObject[] buttons;
#pragma warning disable CS0108 // Składowa ukrywa dziedziczoną składową; brak słowa kluczowego new
    public Renderer renderer;

    public bool panelopen = false;
    public Animator panelanimator;
    public Light flashlight;
    public int tempcamfortest;
    public GameObject pointer;

    public TimeAndPower TimeAndPower;

    public Material nosignal;
    public Material uncharded;

    public int tempcurrent;

    public float laptopbatery;
    public Slider batery;
    public bool cancharge;
    public GameObject knwirus;

    public float baterytokill;

    public AudioSource AudioSource1;
    public AudioSource AudioSource2;
    public AudioSource AudioSource3;

    public bool cooldowned;
    void Start()
    {
        laptopbatery = 100;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        renderer = screen.GetComponent<Renderer>();

        StartCoroutine(knwir());
        cooldowned = true;
    }


    void Update()
    {
        //Debug.Log(currentcamera);
        if (cancharge == true && laptopbatery <= 100)
        {
            laptopbatery = laptopbatery + 16 * Time.deltaTime;
        }
        if (laptopbatery > 100)
        {
            laptopbatery = 100;
        }

        if (knwirus.activeSelf == false)
        {
            batery.value = laptopbatery;
        }
        else
        {
            batery.value = Random.Range(0, 100f);
        }

        if (laptopbatery > 0.1f)
        {
            if (TimeAndPower.power > 0.1f)
            {
                if (panelopen == true && laptopbatery > 0.1f && TimeAndPower.power > 0.1f)
                {
                    if (currentcamera == -1)
                    {
                        currentcamera = 0;
                        changed();
                    }
                    renderer.sharedMaterial = cameras[currentcamera];
                }

            }
            else
            {
                currentcamera = -1;
                renderer.sharedMaterial = nosignal;
            }
        }
        else
        {
            currentcamera = -1;
            renderer.sharedMaterial = uncharded;
        }

        if (panelopen == true)
        {
            if (laptopbatery <= 0)
            {
                laptopbatery = 0;
            }
            else
            {
                laptopbatery = laptopbatery - baterytokill * Time.deltaTime;
            }

        }




        if (Input.GetMouseButtonDown(0) && panelopen == true && TimeAndPower.power > 0.1f && laptopbatery > 0.1f)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);



            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "button2")
                {
                    changed();
                    currentcamera = 0;
                    hit.collider.GetComponent<MeshRenderer>().sharedMaterial = butonsel;
                }
                if (hit.collider.name == "button3")
                {
                    changed();
                    currentcamera = 1;
                    hit.collider.GetComponent<MeshRenderer>().sharedMaterial = butonsel;
                }
                if (hit.collider.name == "button4")
                {
                    changed();
                    currentcamera = 2;
                    hit.collider.GetComponent<MeshRenderer>().sharedMaterial = butonsel;
                }
                if (hit.collider.name == "button5")
                {
                    changed();
                    currentcamera = 3;
                    hit.collider.GetComponent<MeshRenderer>().sharedMaterial = butonsel;
                }
                if (hit.collider.name == "button6")
                {
                    changed();
                    currentcamera = 4;
                    hit.collider.GetComponent<MeshRenderer>().sharedMaterial = butonsel;
                }
                if (hit.collider.name == "button7")
                {
                    changed();
                    currentcamera = 5;
                    hit.collider.GetComponent<MeshRenderer>().sharedMaterial = butonsel;
                }
                if (hit.collider.name == "button8")
                {
                    changed();
                    currentcamera = 6;
                    hit.collider.GetComponent<MeshRenderer>().sharedMaterial = butonsel;
                }
                if (hit.collider.name == "button9")
                {
                    changed();
                    currentcamera = 7;
                    hit.collider.GetComponent<MeshRenderer>().sharedMaterial = butonsel;
                }




            }
        }

        if(panelopen == true && AudioSource3.isPlaying == false && laptopbatery > 1)
        {
            AudioSource3.Play();
        }
        if(panelopen == false)
        {
            AudioSource3.Stop();
        }
        if (laptopbatery < 1)
        {
            AudioSource3.Stop();
        }

        if (Input.GetKeyDown(KeyCode.S) && cooldowned == true)
        {
            cooldowned = false;

            if (panelopen == true)
            {
                //zamknij
                //TimeAndPower.usage--;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                pointer.SetActive(true);
                panelanimator.SetTrigger("closepanel");
                panelopen = false;
                flashlight.enabled = true;
                tempcurrent = currentcamera;
                currentcamera = -1;
                AudioSource1.Play();
                StartCoroutine(wait());

            }
            else
            {
                cancharge = false;
                //otwórz
                //TimeAndPower.usage++;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                pointer.SetActive(false);
                panelanimator.SetTrigger("openpanel");
                panelopen = true;
                flashlight.enabled = false;
                currentcamera = tempcurrent;
                AudioSource1.Play();

            }
            StartCoroutine(cd());

        }
    }
    IEnumerator cd()
    {
        yield return new WaitForSeconds(0.2f);
        cooldowned = true;
    }
    public void changed()
    {
        //Debug.Log(currentcamera);
        //Debug.Log("changed");
        AudioSource2.Play();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Renderer>().sharedMaterial = butonnorm;
        }
    }
    IEnumerator wait()
    {
        if (laptopbatery > 1)
        {
            yield return new WaitForSeconds(2);
            if (panelopen == false)
            {
                cancharge = true;
            }
            else
            {
                cancharge = false;
            }
        }
        else
        {
            yield return new WaitForSeconds(5);
            if (panelopen == false)
            {
                cancharge = true;
            }
            else
            {
                cancharge = false;
            }
        }

    }
    IEnumerator knwir()
    {
        yield return new WaitForSeconds(Random.Range(30f, 70f));
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(30f, 60f));
            while (laptopbatery < 0.1f)
            {
                yield return null;
            }
            knwirus.SetActive(true);

        }
    }
    
}
