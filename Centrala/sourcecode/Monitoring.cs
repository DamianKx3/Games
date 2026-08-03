using UnityEngine;

public class Monitoring : MonoBehaviour
{
    float X;
    float Y;
    public bool isTrigger;
    public Camera Camera;
    public Monitoring Trigger;
    public bool StaticCam;
    public bool Triggered;
    float RemoveXclamp;
    float TempStay;
    void Start()
    {
        X = 15;
        Y = 45;
    }

    // Update is called once per frame
    void Update()
    {
        if(StaticCam == true)
        {
            return;
        }
        if(isTrigger == true)
        {
            if (TempStay <= 0)
            {
                Triggered = false;
            }
            else
            {
                TempStay = TempStay - Time.deltaTime;
            }
            return;
        }
        Y = Y +  Input.GetAxis("Horizontal") * Time.deltaTime * 25;
        X = X +  -Input.GetAxis("Vertical") * Time.deltaTime * 25;
        RemoveXclamp = 40 - Camera.fieldOfView;
        X = Mathf.Clamp(X, 15 - RemoveXclamp / 3, 50);
        Y = Mathf.Clamp(Y, 0, 90);
        transform.eulerAngles = new Vector3(X,Y - 45,0);
        if(Trigger.Triggered == true)
        {
            if(Camera.fieldOfView > 25)
            {
                Camera.fieldOfView = Camera.fieldOfView - Time.deltaTime * 5;
            }
            else
            {
                Camera.fieldOfView = 25;
            }
        }
        else
        {
            if (Camera.fieldOfView < 40)
            {
                Camera.fieldOfView = Camera.fieldOfView + Time.deltaTime * 5;
            }
            else
            {
                Camera.fieldOfView = 40;
            }
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if(isTrigger && other.gameObject.tag == "Target")
        {
            Triggered = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (isTrigger && other.gameObject.tag == "Target")
        {
            TempStay = 0.1f;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isTrigger && other.gameObject.tag == "Target")
        {
            Triggered = false;
        }
    }
}
