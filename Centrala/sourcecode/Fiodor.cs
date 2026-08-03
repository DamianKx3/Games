using UnityEngine;

public class Fiodor : MonoBehaviour
{
    public Controller Controller;
    public int State;
    public Animator DoorsAnim;
    public float timer;
    public float LevelOfDanger;
    public GameObject FiodorSprite;
    public OlgierdEnemy OlgierdEnemy;
    float r;
    public Animator Animator;
    public Animator CamAnim;
    public float DoorsTimer;
    bool ambtry;
    public Ambient amb;
    void Start()
    {
        r = Random.Range(3f,5f);
    }

    // Update is called once per frame
    void Update()
    {

        if (State == 0)
        {

            FiodorSprite.SetActive(false);
            Animator.enabled = false;
            timer = timer + Time.deltaTime;
            if (timer > 7 && Data.invincibility <= 0)
            {
                timer = 0;
                if (Random.Range(0, 100) < LevelOfDanger)
                {
                    Animator.enabled = true;
                    State = 1;
                    FiodorSprite.SetActive(true);
                    Animator.SetInteger("State", 2);
                    timer = 0;

                }
            }
        }
        
        if(State == 3)
        {
            timer = timer + Time.deltaTime;
            if(ambtry == false)
            {
                ambtry = true;
                if (Random.Range(0, 4) == 0)
                {
                    amb.PlayAmb(0);
                }
            }
            if (timer > 3)
            {
                timer = 0;
                if (Random.Range(0, 100) < LevelOfDanger && Controller.HoldingDoors == false)
                {

                    Animator.SetInteger("State", 4);
                    timer = 0;

                }
            }
        }
        if(State == 5)
        {

            timer = timer + Time.deltaTime;
            if (Controller.HoldingDoors == true)
            {


                DoorsTimer = DoorsTimer + Time.deltaTime;
                if(DoorsTimer > 0.5f)
                {
                    DoorsAnim.SetBool("fail", true);

                }
                if (DoorsTimer > 5f)
                {
                    timer = 0;
                    Animator.SetInteger("State", 0);
                    DoorsAnim.SetBool("fail", false );
                    State = 0;
                    DoorsTimer = 0;
                }

            }
            else
            {
                if (DoorsTimer != 0)
                {
                    Controller.tip = "patrz przez judasza w drzwiach, jeżeli będzie bardzo blisko, trzymaj drzwi";
                    Animator.SetInteger("State", 7);
                    Controller.DIED = true;
                    DoorsAnim.SetBool("open", true);
                    CamAnim.SetBool("JumpScare", true);

                }
                else
                {
                    if (timer > 5f)
                    {
                        timer = 0;

                        if (Random.Range(0, 100) < LevelOfDanger && Data.invincibility <= 0)
                        {
                            Controller.tip = "patrz przez judasza w drzwiach, jeżeli będzie bardzo blisko, trzymaj drzwi";
                            if (Settings.TranslateToEng == true) Controller.tip = "look through the peephole, if he is close, hold the doors";
                            Controller.DIED = true;
                            DoorsAnim.SetBool("open", true);
                            CamAnim.SetBool("JumpScare", true);

                            if (Controller.DoorState != 0)
                            {
                                Animator.SetInteger("State", 7);

                            }
                            else
                            {
                                Animator.SetInteger("State", 6);

                            }

                        }
                    }
                }
                
            }
                      
      
        }
    }
    public void SetState(int State1)
    {
        State = State1;
    }
    public void Ded()
    {
        Controller.ShowDeathScreen();
    }
}
