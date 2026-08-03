using UnityEngine;

public class FinalKonon : MonoBehaviour
{
    public float LevelOfDanger;
    public int Pos;
    public Transform[] positions;
    public float timer;
    public Controller controller;
    public GameObject KononSprite;
    public Animator anim;
    public Animator Camanim;
    public Animator klapa;
    public blackoutFinal bof;
    public AudioSource jumpscare;
    void Start()
    {
        controller = FindFirstObjectByType<Controller>();
        LevelOfDanger =  Data.FinalKonon;
    }

    // Update is called once per frame
    void Update()
    {
        if(Pos != 6)
        {
            KononSprite.SetActive(true);
            if (timer > 4)
            {
                timer = 0;
                if(Pos == 1 && controller.burnedOutRooms[Pos] == true)
                {
                    Pos = 4;
                    int rand = Random.Range(0, positions[Pos].childCount);
                    KononSprite.transform.position = positions[Pos].GetChild(rand).transform.position;
                    KononSprite.transform.rotation = positions[Pos].GetChild(rand).transform.rotation;
                }
                else
                {
                    if (Random.Range(0, 100) < LevelOfDanger || controller.burnedOutRooms[Pos] == true)
                    {

                        Move();
                    }
                }

            }
            else
            {
                timer = timer + Time.deltaTime;
            }

        }
        else
        {

            KononSprite.SetActive(false);
            if (timer > 4)
            {
                Debug.Log("HUJ");
                anim.SetInteger("jumpscare", 1);
                klapa.SetBool("open", true);
            }
            else
            {

            }
            if (timer > 6)
            {
                if (controller.kuferstate != 1)
                {
                    if(controller.DIED == false)
                    {
                        jumpscare.Play();
                        bof.StartCoroutine(bof.dupa());
                    }
                    controller.tip = "Patrz na kamery, jeżeli ktoś z korytarza zniknie, to znaczy, że idzie do ciebie, schowaj sie wtedy w skrzyni.";
                    if (Settings.TranslateToEng == true) controller.tip = "Watch the cameras. If someone in the hallway disappears, it means he is coming for you, hide in the chest";
                    controller.DIED = true;
                    anim.SetInteger("jumpscare", 2);
                    Camanim.SetInteger("Jumpscare", 1);

                }
                else
                {
                    if (controller.burnedOutRooms[0] == false)
                    {
                        Pos = 0;
                    }
                    else if(controller.burnedOutRooms[1] == false)
                    {
                        Pos = 1;
                    }
                    else
                    {
                        Pos = 4;
                    }
                    anim.SetInteger("jumpscare", 0);
                    klapa.SetBool("open", false);
                    int rand = Random.Range(0, positions[Pos].childCount);
                    KononSprite.transform.position = positions[Pos].GetChild(rand).transform.position;
                    KononSprite.transform.rotation = positions[Pos].GetChild(rand).transform.rotation;
                }
            }
            else
            {
                timer = timer + Time.deltaTime;
            }
        }
        
    }
    public void Move()
    {
        controller.noRespond = Random.Range(0.7f, 1.5f);
        controller.Cam1Crash = Pos;
        switch (Pos)
        {
            default:
                break;
            case 0:
                Pos = 1;
                break;
            case 1:
                int r = Random.Range(0, 3);
                if (r == 0)
                {
                    if (controller.burnedOutRooms[0] == false)
                    {
                        Pos = 0;
                    }
                    else
                    {
                        Pos = 4;
                    }

                    
                }
                else if (r == 1)
                {
                    Pos = 4;
                }
                else
                {
                    if (controller.burnedOutRooms[2] == false)
                    {
                        Pos = 2;
                    }
                    else
                    {
                        Pos = 4;
                    }

                }
                break;
            case 2:
                Pos = 1;
                break;
            case 3:
                Pos = 4;
                break;
            case 4:
                r = Random.Range(0, 4);
                if (r == 0)
                {
                    if (controller.burnedOutRooms[3] == false)
                    {
                        Pos = 3;
                    }
                    else
                    {
                        Pos = 6;
                    }

                }
                else if (r == 1)
                {
                    if (controller.burnedOutRooms[1] == false)
                    {
                        Pos = 1;
                    }
                    else
                    {
                        Pos = 6;
                    }

                }
                else if (r == 2)
                {

                    if (controller.burnedOutRooms[5] == false)
                    {
                        Pos = 5;
                    }
                    else
                    {
                        Pos = 6;
                    }
                }
                else
                {
                    Pos = 6;
                }
                break;
            case 5:
                Pos = 4;
                break;
        }
        if(Pos == 6)
        {
            timer = 0;
        }
        controller.Cam2Crash = Pos;
        //KononSprite.transform.position = positions[Pos].position;
        // KononSprite.transform.rotation = positions[Pos].rotation;
        int rand = Random.Range(0, positions[Pos].childCount);
        KononSprite.transform.position = positions[Pos].GetChild(rand).transform.position;
        KononSprite.transform.rotation = positions[Pos].GetChild(rand).transform.rotation;
    }
    public void Ded()
    {
        controller.ShowDeathScreen();
    }
}
