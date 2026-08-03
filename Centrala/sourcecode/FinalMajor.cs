using UnityEngine;
using UnityEngine.InputSystem.XR;

public class FinalMajor : MonoBehaviour
{
    public GameObject MajorSprite;
    public int LevelOfDanger;
    public int State;
    public Animator Anim;
    float timer;
    public Controller Controller;
    public Animator CamAnim;
    public AudioSource jumpscare;
    public float rand;
    void Start()
    {
        MajorSprite.SetActive(false);
        LevelOfDanger = Data.FinalMajor;
        rand = Random.Range(8f, 16f);
    }

    // Update is called once per frame
    void Update()
    {
        if(State == 0)
        {
            MajorSprite.SetActive(false);
            if (timer > 4)
            {
                timer = 0;
                if (Random.Range(0, 100) < LevelOfDanger)
                {
                    MajorSprite.SetActive(true);
                    State = Random.Range(1, 3);
                }
            }
            else
            {
                timer = timer + Time.deltaTime;
            }
        }else if (State == 1)
        {
            Anim.SetInteger("State", 1);
        }
        else if (State == 2)
        {
            Anim.SetInteger("State", 2);
        }
        if(State == 1 || State == 2)
        {

            if (timer > rand)
            {
                timer = 0;
                rand = Random.Range(9f, 15f);
                if (State == 1)
                {
                    if (Controller.FinalDoorState == 0)
                    {
                        State = 0;
                        Anim.SetInteger("State", 0);
                    }
                    else
                    {
                        State = 4;

                    }
                }
                else if (State == 2)
                {
                    if (Controller.FinalDoorState == 1)
                    {
                        State = 0;
                        Anim.SetInteger("State", 0);
                    }
                    else
                    {
                        State = 5;

                    }
                }



            }
            else
            {
                timer = timer + Time.deltaTime;
            }
        }
        if (Controller.kuferstate == 0)
        {

            if (State == 4)
            {
                if (Controller.DIED == false)
                {
                    jumpscare.Play();
                }
                Controller.DIED = true;
                Anim.SetInteger("State", 3);
                CamAnim.SetInteger("Jumpscare", 2);
            }
            if (State == 5)
            {
                if (Controller.DIED == false)
                {
                    jumpscare.Play();
                }
                Controller.tip = "miej na uwadzę zarówno lewe jak i prawe drzwi. Zamknięte mogą być tylko jedne z nich.";
                if (Settings.TranslateToEng == true) Controller.tip = "keep an eye on doors, only one of them can be closed at once.";
                Controller.DIED = true;
                Anim.SetInteger("State", 3);
                CamAnim.SetInteger("Jumpscare", 3);
            }
        }
        

    }
    public void ChangeState(int State1)
    {
        State = State1;
    }
    public void Ded()
    {
        Controller.ShowDeathScreen();
    }
}
