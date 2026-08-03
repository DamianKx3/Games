using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class looking : MonoBehaviour
{
    public Controllpanel Controllpanel;
    public float Sensitivity = 100.0f;
    private float Y = 0.0f;
    private float X = 0.0f;
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if(Controllpanel.panelopen == false)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            Y += mouseX * Sensitivity * Time.deltaTime;
            X += mouseY * Sensitivity * Time.deltaTime;
            Quaternion Rotation = Quaternion.Euler(X, Y, 0.0f);
            if (X <= -50)
            {
                X = -50;
            }
            if (X >= 70)
            {
                X = 70;
            }
            transform.rotation = Rotation;
        }
        
    }
}
