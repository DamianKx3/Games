using UnityEngine;

public class Toturial : MonoBehaviour
{
    public Controller controller;
    public OlgierdEnemy olo;
    public Fiodor fiodor;
    public Konon Konon;
    public GameObject[] tasks;
    public int State;
    public GameObject window1;
    void Start()
    {
        if(Data.CurrentLvl != "T")
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Data.CurrentLvl == "T")
        {
            if(State == 10)
            {
                controller.Money = 69;
            }
            else
            {
                controller.Money = 40;
            }

        }
        foreach (var item in tasks)
        {
            item.SetActive(false);
        }
        if (State == 0)
        {
            tasks[0].SetActive(true);
            if (controller.MonitorLook == 1)
            {
                State = 1;
            }
        }
        if(State == 1)
        {
            tasks[1].SetActive(true);
            if(controller.KononOnCamera > 50)
            {
                State = 2;
            }
        }
        if(State == 2)
        {
            tasks[2].SetActive(true);
            if(controller.MonitorLook == 2)
            {
                State = 3;
            }
        }
        if(State == 3)
        {
            tasks[3].SetActive(true);
            if (window1.activeSelf == true)
            {
                State = 4;
            }
        }
        if (State == 4)
        {
            tasks[4].SetActive(true);
            if (controller.videos.Count > 0)
            {
                Konon.sugarLevel = 80;
                State = 5;
            }

        }
        if (State == 5)
        {
            tasks[5].SetActive(true);
            if (Konon.sugarLevel > 85)
            {
                State = 6;
            }
            if(Konon.sugarLevel < 78)
            {
                Konon.sugarLevel = 80;
            }
        }
        if(State == 6)
        {
            tasks[6].SetActive(true);
            if(controller.WindowState == 1)
            {
                olo.State = 1;
                olo.OlgierdSprite.transform.position = olo.startpointPH1[Random.Range(0, olo.startpointPH1.Length)].position;
                olo.targetpoint = olo.endpointPH1[Random.Range(0, olo.endpointPH1.Length)];
                olo.OlgierdSprite.SetActive(true);
                State = 7;
            }
        }
        if(State == 7)
        {
            tasks[7].SetActive(true);
            if (olo.State == 0)
            {
                State = 8;
            }
        }
        if (State == 8)
        {
            tasks[8].SetActive(true);
            fiodor.State = 1;
            fiodor.Animator.enabled = true;
            fiodor.FiodorSprite.SetActive(true);
            fiodor.Animator.SetInteger("State", 2);
            if (controller.DoorState == 1)
            {
                fiodor.State = 3;
                State = 9;

            }
        }
        if (State == 9)
        {
            tasks[9].SetActive(true);
            if (Input.GetKeyDown(KeyCode.D))
            {
                fiodor.LevelOfDanger = 100;
                Debug.Log("pento");
            }
            if(fiodor.State == 0)
            {
                State = 10;
            }
        }
    }
}
