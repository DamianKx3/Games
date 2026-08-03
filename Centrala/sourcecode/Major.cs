using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Major : MonoBehaviour
{
    public GameObject[] sprites;
    public int State;
    public int LevelOfDanger;
    float timer;
    public Controller controller;
    public TextMeshProUGUI errorTxt;
    public Animator Anim;
    public float playtime;
    public GameObject glitch;

    public float Glitchtime;
    float R;
    public List<GameObject> objs;

    void Start()
    {
        LevelOfDanger = Data.Major;
        R = Random.Range(0.2f, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {

        if(State == 0)
        {
            playtime = 0;
            timer = 0;
            foreach(GameObject sprite in sprites)
            {
                sprite.SetActive(false);
            }
            foreach (GameObject a in objs)
            {
                Destroy(a);
            }
            objs.Clear();
            Glitchtime = 0;
        }
        else if(State == 1)
        {
            timer = timer + Time.deltaTime;
            foreach (GameObject sprite in sprites)
            {
                sprite.SetActive(false);
            }
            if(timer > 0.8f)
            {
                timer = 0;
                State = 2;
            }else if(timer > 0.4f)
            {
                Glitchtime = Glitchtime + Time.deltaTime;
                sprites[1].SetActive(true);
                if (Glitchtime > R && objs.Count < 75)
                {
                    objs.Add(Instantiate(glitch, sprites[1].transform));
                    R = Random.Range(0.001f, 0.01f);
                    Glitchtime = 0;
                }
            }
            else
            {
                sprites[0].SetActive(true);


            }

        }else if (State == 2)
        {
            timer = timer + Time.deltaTime;
            if(timer > 2f)
            {
                playtime = playtime + Time.deltaTime;
                Anim.Play("majorboot", -1, playtime / Anim.GetCurrentAnimatorStateInfo(0).length);
            }
            if (timer > 10f)
            {
                State = 0;
            }else if (timer > 2.5f)
            {
                errorTxt.text = "CRITICAL ERROR\n\n\n\nYour computer ran into the trouble and needs to be restarted \n\n\nLATEXP ver.1.7\nerror code:21x37jp";
            }
            else if (timer > 2.1f)
            {


                errorTxt.text = "CRITICAL ERROR\n\n\n\nYour computer ran into the trouble and needs to be restarted";

            }
            else if (timer > 2f)
            {
                errorTxt.text = "CRITICAL ERROR";
                sprites[3].SetActive(true);
                sprites[2].SetActive(false);
                foreach (GameObject a in objs)
                {
                    Destroy(a);
                }
                objs.Clear();
            }
            else
            {
                if (sprites[2].GetComponent<AudioSource>().isPlaying == false)
                {
                    sprites[2].GetComponent<AudioSource>().Play();
                }               
                sprites[2].SetActive(true);
                sprites[1].SetActive(false);
                Glitchtime = Glitchtime + Time.deltaTime;
                if (Glitchtime > R && objs.Count < 75)
                {
                    
                    objs.Add(Instantiate(glitch, sprites[2].transform));
                    R = Random.Range(0.001f, 0.01f);
                    Glitchtime = 0;
                }
            }
        }
    }
    public void Trigger()
    {
        if (State == 0)
        {
            if (Random.Range(0, 100) < LevelOfDanger)
            {
                State = 1;

            }
        }
        else if(State == 1)
        {
            State = 0;
        }
    }
    public void Off()
    {
        if(State != 2)
        {
            timer = 0;
            State = 0;
            foreach (GameObject sprite in sprites)
            {
                sprite.SetActive(false);
            }
        }

    }
}
