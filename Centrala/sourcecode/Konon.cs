using UnityEngine;

public class Konon : MonoBehaviour
{
    public Controller Controller;
    public GameObject KononSprite;
    [Header("poruszanie")]
    public float LevelOfDanger;
    public int Pos;
    public Transform[] positions;
    public float timer;
    [Header("zdrowie")]
    public float sugarLevel;
    public int SugarMode;
    float dropmulti;
    [Header("umieranie")]
    public bool Died;
    public bool RunAway;
    public Police Police;
    void Start()
    {
        sugarLevel = 140;
    }

    // Update is called once per frame
    void Update()
    {
        if (RunAway == true)
        {
            KononSprite.SetActive(false);
        }
        if (Died == true || RunAway == true)
        {
            Police.State = 1;
            return;
        }
        if (Data.powerOut == false)
        {
            if ((sugarLevel < 1 || sugarLevel > 599) && Data.invincibility <= 0)
            {
                Died = true;
                Controller.BlockedScreen.SetActive(true);
            }
            


            //zdrowie
            if (SugarMode == 0) //zdrowy
            {
                if(Data.invincibility <= 0)
                {
                    sugarLevel = sugarLevel - Time.deltaTime * 0.4f;
                }
                else
                {
                    sugarLevel = sugarLevel - Time.deltaTime * 0.1f;
                }
                dropmulti = dropmulti + Time.deltaTime;
                if (dropmulti > 10 && Data.invincibility <= 0)
                {
                    SugarMode = 1;
                    dropmulti = 0;
                }

            }
            else if (SugarMode == 1) // po jakims czasie spada szybcxiej cukier
            {
                sugarLevel = sugarLevel - Time.deltaTime * 0.9f;

            }
            else if (SugarMode == 2) // atak cukrzycowy
            {
                sugarLevel = sugarLevel + Time.deltaTime * 6;
                if(Data.invincibility > 0)
                {
                    SugarMode = 0;
                }

            }
            else if (SugarMode == 3) // insulina
            {
                sugarLevel = sugarLevel - Time.deltaTime * 6;
                if (sugarLevel <= 199)
                {
                    SugarMode = 0;
                }

            }
            //poruszanie sie
            if (Pos != 6)
            {
                if (timer > 5)
                {
                    timer = 0;
                    if (Random.Range(0, 100) < LevelOfDanger)
                    {
                        Move();
                    }
                }
                else
                {
                    timer = timer + Time.deltaTime;
                }
            }
            else
            {
                timer = timer + Time.deltaTime;
                if (timer > 5)
                {
                    if (Random.Range(0, 100) < LevelOfDanger && Controller.CameraNow != 6) // cam 6 to wiatrolap
                    {
                        RunAway = true;
                    }
                    if (Controller.CdoorsClosed == true) // odejscie z wiatrolapu
                    {
                        Pos = 4;
                        Controller.noRespond = Random.Range(0.7f, 1.5f);
                        Controller.Cam1Crash = 4;
                        Controller.Cam2Crash = 6;
                        KononSprite.transform.position = positions[4].transform.position;
                        KononSprite.transform.rotation = positions[4].transform.rotation;
                        timer = 0;
                    }
                }
            }
        }
        else
        {
            if (timer > 2)
            {
                timer = 0;
                if (Random.Range(0, 100) < LevelOfDanger)
                {
                    MovePowerOut();
                }
            }
            else
            {
                timer = timer + Time.deltaTime;
            }
        }
       

    }
    public void DeliverFood(string str) // jedzenie
    {
        float Sugar = float.Parse(str.Split(' ')[0]);
        int price = int.Parse(str.Split(' ')[1]);
        if (Controller.pay(price))
        {
            if (Sugar != -1)
            {
                SugarMode = 0;
                sugarLevel = sugarLevel + Sugar * Random.Range(0.8f, 1.2f);
                if (Random.Range(0, 100f) + Sugar / 4 > 100)
                {
                    SugarMode = 2;
                }
                if (sugarLevel > 199)
                {
                    SugarMode = 2;
                }
            }
            else
            {
                SugarMode = 3;
            }
        }
        


    }
    public void Move()
    {
        Controller.noRespond = Random.Range(0.7f, 1.5f);
        Controller.Cam1Crash = Pos;
        switch (Pos)
        {
            default:
                break;
            case 0:
                Pos = 1;
                break;
            case 1:
                int r = Random.Range(0, 10);
                if (r == 0 || r == 1 || r == 2)
                {
                    Pos = 0;
                }
                else if (r == 3 || r == 4 || r == 5 || r == 9)
                {
                    Pos = 4;
                }
                else
                {
                    Pos = 2;
                }
                break;
            case 2:
                Pos = 1;
                break;
            case 3:
                Pos = 4;
                break;
            case 4:
                r = Random.Range(0, 13);
                if (r == 0 || r == 1 || r == 2)
                {
                    Pos = 3;
                }
                else if (r == 3 || r == 4 || r == 5 || r == 9)
                {
                    Pos = 1;
                } else if (r == 10 || r == 11 || r == 12)
                {
                    if(Data.ActiveDoors == true)
                    {
                        Pos = 6;
                    }
                    else
                    {
                        if(r == 10)
                        {
                            Pos = 1;

                        }else if (r == 11)
                        {
                            Pos = 3;
                        }
                        else if (r == 12)
                        {
                            Pos = 5;
                        }
                    }

                }
                else
                {
                    Pos = 5;
                }
                break;
            case 5:
                Pos = 4;
                break;


        }
        Controller.Cam2Crash = Pos;
        //KononSprite.transform.position = positions[Pos].position;
        // KononSprite.transform.rotation = positions[Pos].rotation;
        int rand = Random.Range(0, positions[Pos].childCount);
        KononSprite.transform.position = positions[Pos].GetChild(rand).transform.position;
        KononSprite.transform.rotation = positions[Pos].GetChild(rand).transform.rotation;
    }
    public void MovePowerOut()
    {
        Controller.noRespond = Random.Range(0.7f, 1.5f);
        Controller.Cam1Crash = Pos;

        switch (Pos)
        {
            default:
                break;
            case 0:
                Pos = 1;
                break;
            case 1:
                Pos = 4;
                break;
            case 2:
                Pos = 1;
                break;
            case 3:
                Pos = 4;
                break;
            case 4:
                Pos = 6;
                break;
            case 5:
                Pos = 4;
                break;
            case 6:
                if (Controller.CameraNow != 6 ||( Controller.MonitorLook != 1 && Controller.CameraNow == 6))
                {
                    RunAway = true;
                }

                break;


        }

        Controller.Cam2Crash = Pos;
        int rand = Random.Range(0, positions[Pos].childCount);
        KononSprite.transform.position = positions[Pos].GetChild(rand).transform.position;
        KononSprite.transform.rotation = positions[Pos].GetChild(rand).transform.rotation;
    }
    public void PlayAudio(int moveto)
    {
        Controller.noRespond = Random.Range(0.7f, 1.5f);
        Controller.Cam1Crash = Pos;
        switch (Pos)
        {
            default:
                break;
            case 0:
                if (moveto == 1)
                {
                    Pos = 1;
                }
                break;
            case 1:
                if (moveto == 0)
                {
                    Pos = 0;
                }
                if (moveto == 2)
                {
                    Pos = 2;
                }
                if (moveto == 4)
                {
                    Pos = 4;
                }
                break;
            case 2:
                if (moveto == 1)
                {
                    Pos = 1;
                }
                break;
            case 3:
                if (moveto == 4)
                {
                    Pos = 4;
                }
                break;
            case 4:
                if (moveto == 1)
                {
                    Pos = 1;
                }
                if (moveto == 3)
                {
                    Pos = 3;
                }
                if (moveto == 5)
                {
                    Pos = 5;
                }
                if (moveto == 6)
                {
                    Pos = 6;
                }
                break;
            case 5:

                if (moveto == 4)
                {
                    Pos = 4;
                }
                break;
            case 6:
                if(moveto == 4)
                {
                    Pos = 4;
                }
                break;


        }
        Controller.Cam2Crash = Pos;
        int rand = Random.Range(0, positions[Pos].childCount);
        KononSprite.transform.position = positions[Pos].GetChild(rand).transform.position;
        KononSprite.transform.rotation = positions[Pos].GetChild(rand).transform.rotation;
    }

}
