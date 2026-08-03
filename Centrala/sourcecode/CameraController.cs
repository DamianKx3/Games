using UnityEngine;

public class CameraController : MonoBehaviour
{
    int maxheight;
    int maxwidth;
    public float additionalX;
    public float additionalY;
    public Vector3 StartPos;
    public int LookState;
    float targetRot;
    public bool Distable;
    public Controller controller;
    float blink1;
    public GameObject blinkobj;
    void Start()
    {
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Distable == true)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            LookState--;
            if(LookState < 0)
            {
                LookState = 3;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            LookState++;
            if(LookState > 3)
            {
                LookState = 0;
            }
        }
        targetRot = LookState * 90;
        
        maxheight = Screen.height;
        maxwidth = Screen.width;

        additionalX = (Input.mousePosition.x-maxwidth / 2) / maxwidth;
        additionalY = (Input.mousePosition.y - maxheight / 2) / maxheight;
        transform.position = StartPos + new Vector3(0, additionalY, 0) + additionalX  * transform.right;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,Mathf.LerpAngle(transform.eulerAngles.y,targetRot,Time.deltaTime * 10),transform.eulerAngles.z);
    }
    public void DoorsMove(int ID)
    {
        controller.DoorsMove(ID);
    }
    public void Window(int state)
    {
        controller.SetWindowState(state);
    }
    public void kuferMove()
    {
        controller.kuferstate = 0;
    }
    public void temp()
    {
        controller.ShowDeathScreen();
    }
    public void OpenWar(int open)
    {
        if(open == 0)
        {
            controller.OpenWar(false);
        }
        else
        {
            controller.OpenWar(true);
        }

    }
    public void blink()
    {
        blinkobj.SetActive(true);
    }
    public void Ded()
    {
        controller.ShowDeathScreen();
    }
}
