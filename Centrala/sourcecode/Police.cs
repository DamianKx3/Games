using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using UnityEngine.InputSystem.HID;

public class Police : MonoBehaviour
{
    public Controller Controller;
    public int State;
    public Animator DoorsAnim;
    public Animator CamAnim;
    public float timer;
    public GameObject Sprite;
    public OlgierdEnemy OlgierdEnemy;
    public Fiodor Fiodor;
    float r;
    public AudioSource flashbang;
    public GameObject Ambient;
    public float RandomAmbient;
    public float t1;
    void Start()
    {
        r = Random.Range(10f, 20f);
        RandomAmbient = Random.Range(120f,500f);
    }

    // Update is called once per frame
    void Update()
    {
        if(t1 > RandomAmbient)
        {
            t1 = t1 + Time.deltaTime;
        }
        else
        {
            t1 = 0;
            RandomAmbient = Random.Range(120f, 500f);
            Ambient.SetActive(true);
        }
        if (State == 1)
        {
            Fiodor.State = 0;
            OlgierdEnemy.State = 0;
            timer = timer + Time.deltaTime;
            if (timer > r - 0.5f)
            {
                Ambient.SetActive(true);

            }
            if(timer > r)
            {
                if(Controller.DIED == false)
                {
                    flashbang.Play();

                }
                CamAnim.SetBool("police", true);
                DoorsAnim.SetBool("open", true);
                Controller.tip = "nagrywaj, montuj filmy i zarabiaj kase, nie pozwól aby poziom cukru spadł do 0 albo powyżej 600. Nie pozwól aby uciekł.";
                if (Settings.TranslateToEng == true) Controller.tip = "Record, edit videos, and make money. Don’t let the sugar level drop to 0 or go above 600.Don’t let him escape.";

                Controller.DIED = true;
                Controller.SkipJumpScareAnim = true;

            }
        }
    }
    public void Ded()
    {
        Controller.ShowDeathScreen();
    }
}
