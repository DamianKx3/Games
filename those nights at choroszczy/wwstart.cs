using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wwstart : MonoBehaviour
{
    public TimeAndPower timeAndPower;
    public Controllpanel Controllpanel;
    public int state;
    public GameObject konon;
    public GameObject major;
    public GameObject meksyk;
    public GameObject jan;
    public GameObject doorcontroller;
    public GameObject lever;
    public GameObject panel;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        state = 0;
        while (state == 0)
        {

        }
    }

}
