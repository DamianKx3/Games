using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

public class FinalJan : MonoBehaviour
{
    public GameObject JanSprite;
    public int LevelOfDanger;
    public int State;
    float timer;
    public Animator animator;
    public bool flashed;
    public float flashingtime;
    int R;
    int R2;
    public Controller Controller;
    public Animator Camanim;
    public blackoutFinal bof;
    public AudioSource jumpscare;
    public FinalKonon Finalkonon;
    void Start()
    {
        LevelOfDanger = Data.Finaljano;
    }

    // Update is called once per frame
    void Update()
    {
        if(State == 0)
        {
            JanSprite.SetActive(false);
            R = 0;
            flashingtime = 0;
            if (timer > 6)
            {
                timer = 0;
                if (Random.Range(0, 100) < LevelOfDanger)
                {
                    State = 1;
                    R2 = Random.Range(3,6);
                    bof.StartCoroutine(bof.dupa());
                }
            }
            else
            {
                timer = timer + Time.deltaTime;
            }
        }
        if(State == 1)
        {
            JanSprite.SetActive(true);

            if(flashingtime > 1)
            {
                animator.SetInteger("State",Random.Range(0,4));
                flashingtime = 0;
                R++;
                if(R >= R2)
                {
                    State = 0;
                    bof.StartCoroutine(bof.dupa());
                    R = 0;
                }
            }
            else
            {
                if (flashed == true)
                {
                    flashingtime = flashingtime + Time.deltaTime;
                }
                else
                {
                    flashingtime = 0;
                }
            }
            if(Finalkonon.Pos == 6)
            {
                timer = 0;
            }
            if(flashed == false)
            {
                if (timer > 5.5f)
                {
                    timer = 0;
                    if (Random.Range(0, 100) < LevelOfDanger)
                    {
                        State = 2;
                    }
                }
                else
                {
                    timer = timer + Time.deltaTime;
                }
            }
        }
        if(State == 2)
        {
            if (Controller.kuferstate == 1)
            {
                animator.SetBool("Inside", true);
            }
            else
            {
                animator.SetBool("Inside", false);
            }
            if (Controller.DIED == false)
            {
                bof.StartCoroutine(bof.dupa());
                jumpscare.Play();
            }
            animator.SetInteger("State", 5);
            Camanim.SetInteger("Jumpscare", 1);
            Controller.tip = "Śledź jego wzrok kursorem aż zniknie.";
            if (Settings.TranslateToEng == true) Controller.tip = "follow his sight by your cursor to make him dissapear.";

            Controller.DIED = true;
        }

        
    }
    public void Ded()
    {

        Controller.ShowDeathScreen();
    }
}
