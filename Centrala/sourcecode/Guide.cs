using UnityEngine;

public class Guide : MonoBehaviour
{
    public Controller controller;
    public CameraController cam;
    public CameraController camfinal;
    public GameObject[] guides;
    void Start()
    {

        if (Data.CurrentLvl == "0" || Data.CurrentLvl == "T")
        {

        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject guide in guides)
        {
            guide.SetActive(false);
        }
        if(Data.FinalMode == false)
        {
            if (controller.MonitorLook == 0 && controller.WindowState == 0 && controller.DoorState == 0)
            {
                if (controller.DoorState == 0)
                {
                    if (cam.LookState == 0)
                    {
                        guides[0].gameObject.SetActive(true);
                    }else if(cam.LookState == 1)
                    {
                        guides[1].gameObject.SetActive(true);
                    }
                    else if(cam.LookState == 2)
                    {
                        guides[2].gameObject.SetActive(true);

                    }
                    else if(cam.LookState == 3)
                    {
                        guides[3].gameObject.SetActive(true);
                    }
                }
            }
            if(controller.MonitorLook == 1 || controller.MonitorLook == 2 || controller.WindowState == 2)
            {
                guides[4].gameObject.SetActive(true);
            }
            if(controller.DoorState == 2)
            {
                guides[5].gameObject.SetActive(true);
            }
            if (controller.DoorState == 3)
            {
                guides[7].gameObject.SetActive(true);
            }
            if (controller.DoorState == 4)
            {
                guides[6].gameObject.SetActive(true);
            }
        }
        else
        {

        }

    }
}
